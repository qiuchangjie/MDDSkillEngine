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
    public class Player_Normal : TargetableObject
    {
        PlayerData PlayerData = null;
        private bool IsPlaying = false;

        private void SetIsPlaying(object sender, GameEventArgs e)
        {
            SelectEntityEventArgs n = (SelectEntityEventArgs)e;

            if (n.entity == this)
            {
                IsPlaying = true;
            }
            else
            {
                IsPlaying = false;
            }
        }

        private void SetAttack(object sender, GameEventArgs e)
        {
            SelectAttackEntityEventArgs n = (SelectAttackEntityEventArgs)e;

            if (n.entity == this)
            {
                return;
            }
            else
            {
                if (IsPlaying)
                {
                    Game.Fsm.GetFsm<Entity>(Id.ToString()).SetData<VarBoolean>(typeof(kaernormalattackState).Name, true);
                }
            }
        }

        public void Use_S(CallbackContext ctx)
        {
            if (!IsPlaying)
                return;

            Game.Fsm.GetFsm<Entity>(Id.ToString()).SetData<VarBoolean>("AkiIdleState", true);
        }

        public override ImpactData GetImpactData()
        {
            return new ImpactData(PlayerData.HP, 200);
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            //实体级输入绑定
            Game.Input.Control.Heros_Normal.S.performed += Use_S;

            //组件初始化
            Game.Buff.CreatBuffSystem(this.Entity.Id.ToString(), this);
            Game.Fsm.CreateFsm<Entity, AkiStateAttribute>(this);
            Game.Skill.CreateSkillSystem<Player_Normal>(this);


            //事件
            Game.Event.Subscribe(SelectEntityEventArgs.EventId, SetIsPlaying);
            Game.Event.Subscribe(SelectAttackEntityEventArgs.EventId, SetAttack);

            //ui
            UIAbilities u = Game.UI.GetUIForm(UIFormId.Ablities) as UIAbilities;
            u.SetEntity(this);

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
            Game.Event.Unsubscribe(SelectEntityEventArgs.EventId, SetIsPlaying);
            Game.Event.Unsubscribe(SelectAttackEntityEventArgs.EventId, SetAttack);
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

        public override void SetState(EntityNormalState state, bool b)
        {
            base.SetState(state, b);
            switch (state)
            {
                case EntityNormalState.RUN:
                    Game.Fsm.GetFsm<Entity>(Id.ToString()).SetData<VarBoolean>(typeof(AkiRunState).Name, b);
                    break;
            }
        }

    }
}