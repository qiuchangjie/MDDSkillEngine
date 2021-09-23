using MDDGameFramework;
using Animancer;
using UnityEngine;

namespace MDDSkillEngine
{
    public class Player : TargetableObject
    {
        Transform target;

        Camera main;

        Blackboard blackboard;

        Blackboard shared_Blackboard;

        IFsm<Enemy> fsm;

        AnimancerComponent animancers;

        private bool isClick;

        private bool isAttact;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            target = GameObject.Find("GameObject").transform;
            main = GameObject.Find("Main Camera").GetComponent<Camera>();
            animancers = GetComponent<AnimancerComponent>();

        }


        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (Input.GetMouseButtonDown(1))
            {
                isClick = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                isClick = true;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                isAttact = true;
                //Instantiate(FirePrefab, FirePoint.position, FirePoint.rotation);
            }           
        }

        private void LateUpdate()
        {
            if (isClick)
            {
                RaycastHit hit;
                if (Physics.Raycast(main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, Constant.Layer.TargetableObjectLayerId))
                {
                    target.position = hit.point;

                    //GameObject obj = Instantiate(clickPrefab);
                    //obj.transform.position = hit.point;

                    GameEnter.Entity.ShowEffect(new EffectData(GameEnter.Entity.GenerateSerialId(), 50000) { name = "ClickMove", Position = hit.point });


                    //PathFindingTest.workAction.Invoke();


                    //for (int i = 0; i < ais.Length; i++)
                    //{
                    //    ais[i].SearchPath();
                    //}

                    //positionFound = true;
                }
                isClick = false;
            }

            if (isAttact)
            {
                //PathFindingTest.attackAction.Invoke();
                isAttact = false;
            }
        }


    }
}