using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using System;

namespace MDDSkillEngine
{
    public class entiity : MonoBehaviour
    {
        IFsm<entiity> fsm = null;

        EventHandler<GameEventArgs> testEvent;
        EventHandler<GameEventArgs> testEvent1;


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
            //fsm = GameEnter.Fsm.CreateFsm<entiity>(this, new GoAround(), new GoOn());
            //  fsm = GameEnter.Fsm.CreateFsm<entiity>(this,new GoAround(),new GoOn());
        }

        

        private void Update()
        {


            if (Input.GetKeyDown(KeyCode.E))
            {
                GameEnter.Event.Subscribe(TestEventArgs.EventId, testEvent);
                GameEnter.Event.Subscribe(TestEventArgs.EventId, testEvent1);

                GameEnter.Event.Fire(this, TestEventArgs.Create("wulalalalalallalalalalalal"));
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


