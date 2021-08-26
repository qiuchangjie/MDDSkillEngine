using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;

namespace MDDSkillEngine
{  
    public class entiity : MonoBehaviour
    {
        IFsm<entiity> fsm=null;


        public List<GameObject> obj = new List<GameObject>();

        private void Start()
        {
            //fsm = GameEnter.Fsm.CreateFsm<entiity>(this, new GoAround(), new GoOn());
            //  fsm = GameEnter.Fsm.CreateFsm<entiity>(this,new GoAround(),new GoOn());
        }

        

        private void Update()
        {


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


