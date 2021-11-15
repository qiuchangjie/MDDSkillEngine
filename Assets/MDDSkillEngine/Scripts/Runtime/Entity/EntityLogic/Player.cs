using MDDGameFramework;
using Animancer;
using UnityEngine;
using Pathfinding;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class Player : TargetableObject
    {
        PlayerData PlayerData = null;

        Transform target;

        Transform fireTransform;

        Camera main;

        Blackboard blackboard;

        Blackboard shared_Blackboard;

        IFsm<Enemy> fsm;

        AnimancerComponent animancers;

        PathFindingTest pathFindingTest;
        IAstarAI ai;

        private bool isClickRight;

        private bool isClickLeft;

        private bool isAttact;

        private bool isQ;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            target = GameObject.Find("GameObject").transform;
            main = GameObject.Find("Main Camera").GetComponent<Camera>();
            animancers = GetComponent<AnimancerComponent>();
            pathFindingTest = GetComponent<PathFindingTest>();
            ai = GetComponent<AIPath>();

            Game.Buff.CreatBuffSystem(this.Entity.Id.ToString(),this);

            Game.HpBar.ShowHPBar(this, 1, 1);

            fireTransform = transform.Find("FirePoint");

            PlayerData = userData as PlayerData;
            if (PlayerData == null)
            {
                Log.Error("PlayerData is invalid.");
                return;
            }

        }


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            SelectEntity.InitPlayer(this);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (Input.GetMouseButtonDown(1))
            {
                isClickRight = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                isClickLeft = true;
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                isAttact = true;
                //Instantiate(FirePrefab, FirePoint.position, FirePoint.rotation);
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                isQ = true;
                //Instantiate(FirePrefab, FirePoint.position, FirePoint.rotation);
            }
        }

        private void LateUpdate()
        {
            if (isClickLeft)
            {
                RaycastHit hit;
                if (Physics.Raycast(main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1<<11))
                {
                    //Log.Error("hitPointSelet:{0}",hit.collider.gameObject.name);

                    Entity e = hit.collider.gameObject.GetComponent<Entity>();



                    if (e!=null)
                    {
                        //Log.Error("获取到entity{0}", e.name);
                        SelectEntity.InitSelectEntity(e);
                    }


                    //target.position = hit.point;

                    //Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 50000) { name = "ClickMove", Position = hit.point });
                }
                isClickLeft = false;
            }

            if (isClickRight)
            {
                RaycastHit hit;
                if (Physics.Raycast(main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1<<0))
                {
                    target.position = hit.point;

                    //GameObject obj = Instantiate(clickPrefab);
                    //obj.transform.position = hit.point;

                    Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 50000) { name = "ClickMove", Position = hit.point });


                    pathFindingTest.workAction.Invoke();


                    //for (int i = 0; i < ais.Length; i++)
                    //{
                    //    ais[i].SearchPath();
                    //}

                    ai.SearchPath();

                    //positionFound = true;
                }
                isClickRight = false;
            }

            if (isAttact)
            {                
                pathFindingTest.attackAction.Invoke();

                Game.Entity.ShowBullet(new BulletData(Game.Entity.GenerateSerialId(), 10, 10, CampType.Enemy, 10, 10)
                {                 
                    name = "Bullet",                    
                    Position = fireTransform.position,
                    Rotation = fireTransform.rotation
                }) ;

                Game.HpBar.ShowHPBar(this,0.1f,0.8f);

                isAttact = false;
            }

            if (isQ)
            {
                //Game.Buff.AddBuff(this.Id.ToString(),"Buff",this,this);

                Game.HpBar.ShowHPBar(this,1,0);

                isQ = false;
            }
        }


    }
}