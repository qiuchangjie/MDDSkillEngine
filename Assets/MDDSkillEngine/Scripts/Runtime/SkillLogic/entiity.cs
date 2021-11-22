using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using System;
using CatAsset;
using MDDGameFramework.Runtime;
using XNode;

namespace MDDSkillEngine
{
    public class entiity : MonoBehaviour
    {
        IFsm<entiity> fsm = null;

        private Root behaviorTree;

      

        public NodeGraph nodeGraph;

        EventHandler<GameEventArgs> testEvent;
        EventHandler<GameEventArgs> testEvent1;

        Action<object> LoadAssetSucess;

        public List<GameObject> obj = new List<GameObject>();

        private void LogString(object sender, GameEventArgs e)
        {
            TestEventArgs ne = (TestEventArgs)e;

            Debug.LogError("TestEventArgs.string:" + ne.logString + "id:" + ne.Id);
        }

        private void debugstring(object sender, GameEventArgs e)
        {
            TestEventArgs ne = (TestEventArgs)e;

            Debug.LogError("TestEventArgs.string:" + ne.logString + "id:" + ne.Id);
        }


        private void Start()
        {
            testEvent += LogString;
            testEvent1 += debugstring;
            LoadAssetSucess += loadsucess;
            //fsm = Game.Fsm.CreateFsm<entiity>(this, new GoAround(), new GoOn());
            //  fsm = Game.Fsm.CreateFsm<entiity>(this,new GoAround(),new GoOn());
        }

        List<GameObject> objs = new List<GameObject>();  

        public void loadsucess(object obj)
        {
            objs.Add(Instantiate((GameObject)obj));

            Debug.LogError(CatAssetManager.assetBundleInfoDict.Count);
           
        }

        private void TestBehaviourTreeFactory()
        {
            List<NodePort> nodePorts = new List<NodePort>();

            foreach (var item in ((RootNode)nodeGraph.nodes[0]).Outputs)
            {
                nodePorts.Add(item);
            }

            ((RootNode)nodeGraph.nodes[0]).root.SetClock();

            ((RootNode)nodeGraph.nodes[0]).root.SetMainNode(((ActionTest)nodePorts[0].Connection.node).action);

            ((RootNode)nodeGraph.nodes[0]).root.SetDecoratee();

            ((RootNode)nodeGraph.nodes[0]).root.SetRoot(((RootNode)nodeGraph.nodes[0]).root);

            ((ActionTest)nodePorts[0].Connection.node).action.SetParent(((RootNode)nodeGraph.nodes[0]).root);

            ((ActionTest)nodePorts[0].Connection.node).action.SetAction(((ActionTest)nodePorts[0].Connection.node).debug);

            ((RootNode)nodeGraph.nodes[0]).root.Start();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Debug.LogError("nodeGraph.nodes.Count" + nodeGraph.nodes.Count);

               // Debug.LogError($"{((RootNode)nodeGraph.nodes[0]).root.Name}------------{((RootNode)nodeGraph.nodes[0]).Outputs}----------");

                //TestBehaviourTreeFactory();

                Game.Entity.ShowEntity<Cubes>(100,"Assets/Prefab/Model1/Akiiii.prefab", "player");
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                //CatAssetManager.LoadAsset("Assets/Prefab/Model1/Aki1.prefab", LoadAssetSucess);
                Game.Entity.AddEntityGroup("player", 10, 10, 10, 10);
            }


            if (Input.GetKeyDown(KeyCode.J))
            {
                AssetRuntimeInfo dependencyInfo = CatAssetManager.assetInfoDict["Assets/Prefab/Model1/Akiiii.prefab"];

                CatAssetManager.UnloadAsset(dependencyInfo.Asset);

                Debug.LogError(CatAssetManager.assetBundleInfoDict.Count);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                //fsm = Game.Fsm.CreateFsm<entiity>(this, new GoAround(), new GoOn());

                //fsm.Start<GoAround>();

                //BuffFactory.AcquireBuff("Buff");
            }

            if (Input.GetKeyDown(KeyCode.H))
            {
                if (fsm.GetState<GoOn>() == fsm.CurrentState)
                {
                    fsm.CurrentState.ChangeState<GoAround>(fsm);
                }
                else
                {
                    fsm.CurrentState.ChangeState<GoOn>(fsm);
                }            
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                behaviorTree = Game.NPBehave.CreatBehaviourTree(
                    new MDDGameFramework.Action(()=> { Debug.LogError("CreatBehaviourTree"); }));

                behaviorTree.Start();
            }
        }

    }


    /// <summary>
    /// GF需要的实体逻辑脚本
    /// 在运行时会动态的添加到实体身上
    /// </summary>
    public class Cubes : EntityLogic
    {

    }
}


