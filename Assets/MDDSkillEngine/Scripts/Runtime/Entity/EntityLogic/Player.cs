using MDDGameFramework;
using Animancer;
using UnityEngine;
using Pathfinding;
using MDDGameFramework.Runtime;
using UnityEngine.InputSystem;
using System;
using static UnityEngine.InputSystem.InputAction;

namespace MDDSkillEngine
{
    public class Player : TargetableObject
    {

        Action<CallbackContext> Skill_1;

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
        public void UseSkill_1(CallbackContext ctx)
        {
            if (Game.Fsm.GetFsm<Player>(Id.ToString()).GetCurrStateName() != "AkiShunXiState")
            {
                Game.Skill.GetSkillSystem(Id).UseSkill(10001);

                if (SelectUtility.MouseRayCastByLayer(1 << 0 + 1 << 1, out RaycastHit hit))
                {
                    Game.Select.currentClick = hit.point;
                }
            }
        }

        public void UseSkill_2(CallbackContext ctx)
        {
            //Game.Fsm.GetFsm<Player>(Id.ToString()).SetData<VarBoolean>("jianrenfengbao", true);
            Game.Skill.GetSkillSystem(Id).UseSkill(10004);
        }

        public void UseSkill_3(CallbackContext ctx)
        {
            //Game.Fsm.GetFsm<Player>(Id.ToString()).SetData<VarBoolean>("skilldatatest", true);
            Game.Skill.GetSkillSystem(Id).UseSkill(10005);
        }

        public void OnClickLeft(CallbackContext ctx)
        {
            
        }

        public void OnClickRight(CallbackContext ctx)
        {
            if (SelectUtility.MouseRayCastByLayer(1 << 0 | 1 << 1, out RaycastHit vector3))
            {
                Game.Select.pathFindingTarget.transform.position = vector3.point;
                move.SearchPath();
                Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70000) { Position = vector3.point });
            }
        }

        
        public override ImpactData GetImpactData()
        {
            return new ImpactData(PlayerData.HP, 200);
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);


            Game.Input.Control.Heros_Normal.Skill_1.performed += UseSkill_1;
            Game.Input.Control.Heros_Normal.Skill_2.performed += UseSkill_2;
            Game.Input.Control.Heros_Normal.Skill_3.performed += UseSkill_3;
            Game.Input.Control.Heros_Normal.RightClick.performed += OnClickRight;

            Game.Buff.CreatBuffSystem(this.Entity.Id.ToString(),this);
            Game.Fsm.CreateFsm<Player, AkiStateAttribute>(this);
            move = GetComponent<PlayerMovement>();
           
            Game.Skill.CreateSkillSystem<Player>(this);

            Game.Skill.GetSkillSystem(Id).AddSkill(10001);
            Game.Skill.GetSkillSystem(Id).AddSkill(10004);
            Game.Skill.GetSkillSystem(Id).AddSkill(10005);
            Game.Skill.GetSkillSystem(Id).AddSkill(10006);


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

            Game.HpBar.ShowHPBar(this, 1, 1);

            Game.Select.InitPlayer(this);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);


            Game.HpBar.HideHPBar(this);

            //IFsm<Player> fsm = Game.Fsm.GetFsm<Player>(Entity.Id.ToString());          
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            InputLogic();

            //switch (SelectState)
            //{
            //    case EntitySelectState.None:
            //        CacheOutLiner.SetOutLiner(false);
            //        break;
            //    case EntitySelectState.OnHighlight:
            //        CacheOutLiner.SetOutLiner(true);
            //        break;
            //    case EntitySelectState.OnSelect:
            //        CacheOutLiner.SetOutLiner(false);
            //        break;
            //}

            //RaycastHit hit;
            //if (Physics.Raycast(Game.Scene.MainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1 << 8))
            //{
            //    Entity e = hit.collider.gameObject.GetComponent<Entity>();
            //    if (e != null)
            //    {
            //        if (e == this)
            //        {
            //            SwitchEntitySelectState(EntitySelectState.OnHighlight);
            //        }
            //        else
            //        {
            //            SwitchEntitySelectState(EntitySelectState.None);
            //        }
            //    }
            //    else
            //    {
            //        SwitchEntitySelectState(EntitySelectState.None);
            //    }
            //}
            //else
            //{
            //    SwitchEntitySelectState(EntitySelectState.None);
            //}
        }

        private void OnMouseEnter()
        {
            SwitchEntitySelectState(EntitySelectState.OnHighlight);
        }

        private void OnMouseExit()
        {
            SwitchEntitySelectState(EntitySelectState.None);
        }

        private void InputLogic()
        {
            if (Keyboard.current.vKey.wasPressedThisFrame)
            {
                Visible = !Visible;
            }

        }


    }
}