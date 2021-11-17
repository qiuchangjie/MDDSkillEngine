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

        #region 输入用bool值
        private bool isClickRight;

        private bool isClickLeft;

        private bool isAttact;

        private bool isQ;
        #endregion

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
         
            Game.Buff.CreatBuffSystem(this.Entity.Id.ToString(),this);

            Game.Fsm.CreateFsm<Player, AkiStateAttribute>(this);

            Game.HpBar.ShowHPBar(this, 1, 1);
         

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

            CachedAnimancer.Play(CachedAnimContainer.GetAnimation("Idle"));

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
                if (Physics.Raycast(Game.Scene.MainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1<<11))
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
                if (Physics.Raycast(Game.Scene.MainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1<<0))
                {
                    Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 50000) { name = "ClickMove", Position = hit.point });                 
                }
                isClickRight = false;
            }

            if (isAttact)
            {                
                Game.Entity.ShowBullet(new BulletData(Game.Entity.GenerateSerialId(), 10, 10, CampType.Enemy, 10, 10)
                {                 
                    name = "Bullet",                    
                  
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