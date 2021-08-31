using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using System;
using CatAsset;

namespace MDDSkillEngine
{
    public class entiity : MonoBehaviour
    {
        IFsm<entiity> fsm = null;

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
            //fsm = GameEnter.Fsm.CreateFsm<entiity>(this, new GoAround(), new GoOn());
            //  fsm = GameEnter.Fsm.CreateFsm<entiity>(this,new GoAround(),new GoOn());
        }

        List<GameObject> objs = new List<GameObject>();  

        public void loadsucess(object obj)
        {
            objs.Add(Instantiate((GameObject)obj));

            Debug.LogError(CatAssetManager.assetBundleInfoDict.Count);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                CatAssetManager.LoadAsset("Assets/Prefab/Model1/Akiiii.prefab", LoadAssetSucess);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                CatAssetManager.LoadAsset("Assets/Prefab/Model1/Aki1.prefab", LoadAssetSucess);
            }


            if (Input.GetKeyDown(KeyCode.J))
            {
                AssetRuntimeInfo dependencyInfo = CatAssetManager.assetInfoDict["Assets/Prefab/Model1/Akiiii.prefab"];

                CatAssetManager.UnloadAsset(dependencyInfo.Asset);

                Debug.LogError(CatAssetManager.assetBundleInfoDict.Count);
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                fsm = GameEnter.Fsm.CreateFsm<entiity>(this, new GoAround(), new GoOn());

                fsm.Start<GoAround>();
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
        }

    }
}


