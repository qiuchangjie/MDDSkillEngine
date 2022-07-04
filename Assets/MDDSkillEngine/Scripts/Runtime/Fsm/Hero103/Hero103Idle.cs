using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [Hero103]
    public class Hero103Idle : MDDFsmState<Entity>
    {
        private ClipState.Transition idle;

        public override bool IsButtomState
        {
            get
            {
                return true;
            }
        }

        public override StateType StateType
        {
            get
            {
                return StateType.IDLE;
            }
        }

        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);
            Fsm = fsm;
            Log.Info("{0}创建{1}状态", LogConst.FSM, GetType().Name);
            idle = fsm.Owner.CachedAnimContainer.GetAnimation("103_m_a_standby");
        }
        
        protected override void OnEnter(IFsm<Entity> fsm)
        {
            //base.OnEnter(fsm);
            Log.Info("{0}进入{1}状态", LogConst.FSM, GetType().Name);
            fsm.SetData<VarBoolean>(GetType().Name, false);
            fsm.Owner.CachedAnimancer.Play(idle);
        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("{0}摧毁{1}状态", LogConst.FSM, GetType().Name);
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("{0}离开{1}状态", LogConst.FSM, GetType().Name);
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        }

        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
            VarBoolean varBoolean = (VarBoolean)newValue;

            if (varBoolean.Value == false)
                return;

            ISkillSystem skillSystem = Game.Skill.GetSkillSystem(Fsm.Owner.Id);

            if (skillSystem.GetSkillReleaseResultType() == SkillReleaseResultType.NONE)
            {
                ChangeState(Fsm, GetType());
            }
            else
            {
                skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.STOP);
                ChangeState(Fsm, GetType());
            }
        }
    }
}

