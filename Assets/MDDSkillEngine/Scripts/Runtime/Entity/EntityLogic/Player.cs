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

        PlayerData PlayerData = null;

        PlayerMovement move;   
        public void Use_S(CallbackContext ctx)
        {
            Game.Fsm.GetFsm<Entity>(Id.ToString()).SetData<VarBoolean>("AkiIdleState", true);
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
            
            Game.Input.Control.Heros_Normal.RightClick.performed += OnClickRight;
            Game.Input.Control.Heros_Normal.S.performed += Use_S;

            Game.Buff.CreatBuffSystem(this.Entity.Id.ToString(),this);
            Game.Fsm.CreateFsm<Entity, AkiStateAttribute>(this);
            move = GetComponent<PlayerMovement>();          
            Game.Skill.CreateSkillSystem<Player>(this);
        
            UIBlackboard uIBlackboard = Game.UI.GetUIForm(UIFormId.Blackboard) as UIBlackboard;
            ISkillSystem skillSystem = Game.Skill.GetSkillSystem(1001);
            uIBlackboard.InitData(skillSystem.GetPubBlackboard());

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

            IFsm<Entity> fsm = Game.Fsm.GetFsm<Entity>(Entity.Id.ToString());

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