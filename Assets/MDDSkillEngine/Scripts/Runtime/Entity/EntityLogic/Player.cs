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

        PlayerMovement move;


        #region 输入用bool值
        private bool isClickRight;

        private bool isClickLeft;

        private bool isAttact;

        private bool isQ;

        private bool isW;

        private bool isE;

        private bool isR;
        #endregion

        public override ImpactData GetImpactData()
        {
            return new ImpactData(PlayerData.HP, 200);
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            Game.Buff.CreatBuffSystem(this.Entity.Id.ToString(),this);
            Game.Fsm.CreateFsm<Player, AkiStateAttribute>(this);
            move = GetComponent<PlayerMovement>();
            Game.HpBar.ShowHPBar(this, 1, 1);
            Game.Skill.CreateSkillSystem(this);

            Game.Skill.GetSkillSystem(Id).AddSkill(10001);


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

            Name = "Aki__";

            IFsm<Player> fsm = Game.Fsm.GetFsm<Player>(Entity.Id.ToString());

            fsm.Start<AkiIdleState>();

            Game.Select.InitPlayer(this);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            InputLogic();
        }

        private void InputLogic()
        {
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
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                isQ = true;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                isW = true;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                isE = true;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                isR = true;
            }
        }


        private void LateUpdate()
        {
            if (isClickLeft)
            {
                RaycastHit hit;
                if (Physics.Raycast(Game.Scene.MainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1<<11))
                {
                    //Log.Error("hitPointSelet:{0}",hit.collider.gameObject.name);

                    Entity e = hit.collider.gameObject.GetComponent<Entity>();
                    if (e!=null)
                    {
                        //Log.Error("获取到entity{0}", e.name);
                        Game.Select.InitSelectEntity(e);
                    }
                    //target.position = hit.point;

                    //Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 50000) { name = "ClickMove", Position = hit.point });
                }
                isClickLeft = false;
            }

            if (isClickRight)
            {
                RaycastHit hit;
                if (Physics.Raycast(Game.Scene.MainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1<<0))
                {
                    Game.Select.pathFindingTarget.transform.position = hit.point;

                    move.SearchPath();

                    Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70000) {  Position = hit.point });                 
                }
                isClickRight = false;
            }

            if (isAttact)
            {
                Game.Fsm.GetFsm<Player>(Id.ToString()).SetData<VarBoolean>("attack1",true);
                //Game.Entity.ShowBullet(new BulletData(Game.Entity.GenerateSerialId(), 10, 10, CampType.Enemy, 10, 10)
                //{                 
                //    name = "Bullet",                    
                  
                //}) ;
                //Game.HpBar.ShowHPBar(this,0.1f,0.8f);
                isAttact = false;
            }

            if (isQ && Game.Fsm.GetFsm<Player>(Id.ToString()).GetCurrStateName() != "AkiShunXiState")
            {
                Game.Skill.GetSkillSystem(Id).GetSkill(10001).GetBlackboard().Set<VarBoolean>("input", true);

                RaycastHit hit;
                if (Physics.Raycast(Game.Scene.MainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1 << 0))
                {
                    Game.Select.currentClick = hit.point;
                }

                isQ = false;
            }

            if (isW)
            {
                //Game.Skill.GetSkillSystem(Id).GetSkill(10001).GetBlackboard().Set<VarBoolean>("input",true);

                isW = false;
            }
        }


    }
}