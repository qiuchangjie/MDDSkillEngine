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
    public class Hero103_TimeDemo : TargetableObject
    {
        HeroData PlayerData = null;
        private bool IsPlaying = false;
        private bool isCreate = false;
        private float duration = 0f;
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
                    Game.Fsm.GetFsm<Entity>(Id.ToString()).SetData<VarBoolean>(typeof(Hero103Attack).Name, true);
                }
            }
        }

        public void Use_S(CallbackContext ctx)
        {
            if (!IsPlaying)
                return;

            Game.Fsm.GetFsm<Entity>(Id.ToString()).SetData<VarBoolean>(typeof(Hero103Idle).Name, true);
        }

        public override ImpactData GetImpactData()
        {
            return new ImpactData();
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

           


            //实体级输入绑定
            //Game.Input.Control.Heros_Normal.S.performed += Use_S;

            //组件初始化

            //事件
            //Game.Event.Subscribe(SelectEntityEventArgs.EventId, SetIsPlaying);
            //Game.Event.Subscribe(SelectAttackEntityEventArgs.EventId, SetAttack);

            //ui
            //UIAbilities u = Game.UI.GetUIForm(UIFormId.Ablities) as UIAbilities;
            //u.SetEntity(this);


        }

        protected override void OnRecycle()
        {
            base.OnRecycle();
            
            duration = 0f;
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            Name = "Hero103_TimeDemo";

            UnityEngine.Profiling.Profiler.BeginSample("CreatBuffSystem");
            Game.Buff.CreatBuffSystem(this.Entity.Id.ToString(), this);
            UnityEngine.Profiling.Profiler.EndSample();

            UnityEngine.Profiling.Profiler.BeginSample("CreateFsm");
            Game.Fsm.CreateFsm<Entity, Hero103Attribute>(this);
            UnityEngine.Profiling.Profiler.EndSample();

            UnityEngine.Profiling.Profiler.BeginSample("CreateSkillSystem");
            Game.Skill.CreateSkillSystem<Entity>(this);
            UnityEngine.Profiling.Profiler.EndSample();

            IFsm<Entity> fsm = Game.Fsm.GetFsm<Entity>(Entity.Id.ToString());
            fsm.Start<Hero103Spawn>();

            PlayerData = userData as HeroData;
            if (PlayerData == null)
            {
                Log.Error("PlayerData is invalid.");
                return;
            }

            if (PlayerData.m_Owner != null)
            {
                Game.Entity.AttachEntity(Id, PlayerData.m_Owner.Id);
                CachedTransform.localRotation = PlayerData.localRotation;
                CachedTransform.localPosition = PlayerData.localeftPostion;
                CachedTransform.localScale = PlayerData.localScale;
                Game.Entity.DetachEntity(Id);
            }

            Game.Buff.AddBuff(Id.ToString(), "PlaybleSpeedTestBuff", this, null);

            //Game.HpBar.ShowHPBar(this, 1, 1);
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);

            PlayerData = null;

            //IBuffSystem buffSystem = Game.Buff.GetBuffSystem(Id.ToString());
            //if (buffSystem != null)
            //{
            //    buffSystem.RemoveAllBuff();
            //}

            Game.Input.Control.Heros_Normal.S.performed -= Use_S;

            Game.Buff.RemoveBuffSystem(Id.ToString());
            Game.Fsm.DestroyFsm<Entity>(Id.ToString());
            Game.Skill.RemoveSkillSystem(Id);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            duration += elapseSeconds;

            if (duration >= 15f)
            {
                Game.Entity.HideEntity(this);
            }

            InputLogic();
        }


        private void InputLogic()
        {
            if (Keyboard.current.vKey.wasPressedThisFrame)
            {
                Visible = !Visible;
            }
        }

        public override void ObservingPlayableSpeed(Blackboard.Type type, Variable newValue)
        {
            base.ObservingPlayableSpeed(type, newValue);
            if (type == Blackboard.Type.CHANGE)
            {
                VarFloat varFloat = (VarFloat)newValue;
                IBuffSystem buffsystem = Game.Buff.GetBuffSystem(Id.ToString());
                if (buffsystem != null)
                {
                    buffsystem.PlayableSpeed = varFloat.Value;
                }

                IFsm<Entity> fsm = Game.Fsm.GetFsm<Entity>(Id.ToString());
                fsm.PlayableSpeed = varFloat.Value;
            }

        }

        public override void SetState(EntityNormalState state, bool b)
        {
            base.SetState(state, b);
            switch (state)
            {
                case EntityNormalState.RUN:
                    Game.Fsm.GetFsm<Entity>(Id.ToString()).SetData<VarBoolean>(typeof(Hero103SpaceWalk).Name, b);
                    break;
                case EntityNormalState.FLYSKY:
                    Game.Fsm.GetFsm<Entity>(Id.ToString()).SetData<VarBoolean>(typeof(Hero103FlyUp).Name, b);
                    break;
                case EntityNormalState.SPACEWALK:
                    Game.Fsm.GetFsm<Entity>(Id.ToString()).SetData<VarBoolean>(typeof(Hero103SpaceWalk).Name, b);
                    break;
            }
        }

    }
}