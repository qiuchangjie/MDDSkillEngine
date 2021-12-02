
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

        public void PreLoad()
        {
            
        }
      

        public NP_Tree CreatBehaviourTree(string Name, object userData)
        {
            NPBehaveGraph nP;
            if (!m_GraphDic.TryGetValue(AssetUtility.GetSkillAsset(Name), out nP))
            {
                Log.Error("行为树文件：{0}  不存在", Name);
                return null;
            }

            Root root = null;
            foreach (var v in nP.nodes)
            {
                NP_NodeBase data = v as NP_NodeBase;

                switch (data.nodeType)
                {
                    case NodeType.Task:
                        data.NP_GetNodeData().CreateTask(null, null);
                        break;
                    case NodeType.Decorator:
                        Node node = null;
                        foreach (var v1 in data.Outputs)
                        {
                            node = (v1.Connection.node as NP_NodeBase).NP_GetNodeData().NP_GetNode();
                        }

                        data.NP_GetNodeData().CreateDecoratorNode(null, null, node);

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
                        data.NP_GetNodeData().CreateComposite(nodes.ToArray());
                        break;
                }

             
                
            }

            root = (nP.GetRootNode() as NP_NodeBase).NP_GetNodeData().NP_GetNode() as Root;
            root.SetRoot(root);
            root.SetBlackBoard(Blackboard.Create(nP.BBValues, root.Clock));


            Skill skill = new Skill();
            skill.Init(root);

            return skill;
        }


        private void LoadAssetSuccessCallback(string entityAssetName, object entityAsset, float duration, object userData)
        {
            NPBehaveGraph np = entityAsset as NPBehaveGraph;

            if (np == null)
            {
                Log.Error("行为树文件：{0} 读取失败", entityAssetName);
            }

            Log.Info("行为树文件：{0}  成功", entityAssetName);
            m_GraphDic.Add(entityAssetName, np);
        }

    }



}

