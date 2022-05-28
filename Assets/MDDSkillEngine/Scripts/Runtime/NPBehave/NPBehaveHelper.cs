
using MDDGameFramework;
using System;
using MDDGameFramework.Runtime;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MDDSkillEngine
{
    public class NPBehaveHelper : IBehaveHelper
    {
        private LoadAssetCallbacks assetCallbacks;

        private NPBehaveGraph curGraph;

        private Dictionary<string, NPBehaveGraph> m_GraphDic;


        public NPBehaveHelper()
        {
            assetCallbacks = new LoadAssetCallbacks(LoadAssetSuccessCallback);
            m_GraphDic = new Dictionary<string, NPBehaveGraph>();
        }

        /// <summary>
        /// 加载行为树文件方法
        /// </summary>
        public void PreLoad()
        {
            IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();

            DRSkill[] dRSkills = dtSkill.GetAllDataRows();

            for (int i = 0; i < dRSkills.Length; i++)
            {
                Game.Resource.LoadAsset(AssetUtility.GetSkillAsset(dRSkills[i].AssetName), assetCallbacks);
            }
        }


        public NP_Tree CreatBehaviourTree(string Name, object userData, NPType nPType = NPType.skill)
        {
            NPBehaveGraph nP;
            if (!m_GraphDic.TryGetValue(AssetUtility.GetSkillAsset(Name), out nP))
            {
                Log.Error("行为树文件：{0}  不存在", Name);
                return null;
            }

            //用来辅助缓存行为树节点 以便回收管理
            List<Node> nodes1=new List<Node>();

            Skill skill = new Skill();
            Root root = null;
            foreach (var v in nP.nodes)
            {
                NP_NodeBase data = v as NP_NodeBase;

                switch (data.nodeType)
                {
                    case NodeType.Task:
                        nodes1.Add(data.NP_GetNodeData().CreateTask(userData, skill));
                        break;
                    case NodeType.Decorator:
                        Node node = null;
                        foreach (var v1 in data.Outputs)
                        {
                            node = (v1.Connection.node as NP_NodeBase).NP_GetNodeData().NP_GetNode();
                        }

                        //data.NP_GetNodeData().CreateDecoratorNode(userData, skill, node);
                        //根节点加入辅助管理列表末尾 以便回收的时候最后销毁
                        if (data.NP_GetNodeData().CreateDecoratorNode(userData, skill, node) is Root)
                        {
                            data.NP_GetNodeData().CreateDecoratorNode(userData, skill, node);
                        }
                        else
                        {
                            nodes1.Add(data.NP_GetNodeData().CreateDecoratorNode(userData, skill, node));
                        }
                        
                        break;
                    case NodeType.Composite:
                        List<Node> nodes = new List<Node>();
                        foreach (var v1 in data.Outputs)
                        {
                            foreach (var v2 in v1.GetConnections())
                            {
                                nodes.Add((v2.node as NP_NodeBase).NP_GetNodeData().NP_GetNode());
                            }
                        }
                        //toarray是暴力copy所以这里需要优化
                        //data.NP_GetNodeData().CreateComposite(nodes.ToArray());
                        nodes1.Add(data.NP_GetNodeData().CreateComposite(nodes.ToArray()));
                        break;
                }
            }


            root = (nP.GetRootNode() as NP_NodeBase).NP_GetNodeData().NP_GetNode() as Root;
            root.SetRoot(root);
            nodes1.Add(root);

            //暂时只有kaer公共黑板，暂时这么写
            KaelBlackboard kaelBlackboard = nP.PublicBB as KaelBlackboard;
            if (kaelBlackboard != null)
            {
                Entity entity = userData as Entity;
                ISkillSystem skillSystem = Game.Skill.GetSkillSystem(entity.Id);

                if (skillSystem.GetPubBlackboard() == null)
                {
                    Blackboard blackboard = Blackboard.Create(kaelBlackboard.BBValues, root.Clock);
                    skillSystem.SetBlackboard(blackboard);
                    root.SetBlackBoard(Blackboard.Create(nP.BBValues, blackboard, root.Clock));
                    Log.Info("{0}设置公共黑板成功", LogConst.NPBehave);
                }
                else
                {
                    root.SetBlackBoard(Blackboard.Create(nP.BBValues, skillSystem.GetPubBlackboard(), root.Clock));
                    Log.Info("{0}设置{1}公共黑板成功", LogConst.NPBehave, LogConst.NPBehave);

                }
            }
            else
            {
                root.SetBlackBoard(Blackboard.Create(nP.BBValues, root.Clock));
            }



            Entity owner = userData as Entity;
            if (owner == null)
            {
                Log.Error("没有传入行为树归属者");
            }
            root.SetOwner(owner.Entity);
            skill.SetRootNode(root);

            skill.SetNodeList(nodes1);

            return skill;
        }


        private void LoadAssetSuccessCallback(string entityAssetName, object entityAsset, float duration, object userData)
        {
            NPBehaveGraph np = entityAsset as NPBehaveGraph;

            if (np == null)
            {
                Log.Error("行为树文件：{0} 读取失败", entityAssetName);
            }

            Log.Info("加载行为树文件：{0}  成功", entityAssetName);
            m_GraphDic.Add(entityAssetName, np);
        }
    }



}

