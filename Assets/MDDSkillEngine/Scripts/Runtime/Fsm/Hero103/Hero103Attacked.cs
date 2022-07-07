﻿using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    [Hero103]
    public class Hero103Attacked : MDDFsmState<Entity>
    {
        private ClipState.Transition Damage;


        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建ai   damage模式 ");
            Damage = fsm.Owner.CachedAnimContainer.GetAnimation("103_m_a_attacked");
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("{0}进入ai{1}", LogConst.FSM, GetType().Name);

            fsm.SetData<VarBoolean>(GetType().Name, false);

            if (fsm.Owner.CachedAnimancer.IsPlaying(Damage))
            {
                fsm.Owner.CachedAnimancer.Stop(Damage);
                fsm.Owner.CachedAnimancer.Play(Damage);
            }
            else
            {
                fsm.Owner.CachedAnimancer.Play(Damage);
            }

        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("{0}离开ai{1}", LogConst.FSM, GetType().Name);
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (duration >= 1f)
            {
                Finish(fsm);
            }
        }

        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
            VarBoolean varBoolean = (VarBoolean)newValue;

            if (varBoolean.Value == false)
                return;

            if (((MDDFsmState<Entity>)Fsm.CurrentState).StateType == StateType.IDLE||Fsm.CurrentState == this)
            {
                ChangeState(Fsm,GetType());
            }

        }
    }
}

