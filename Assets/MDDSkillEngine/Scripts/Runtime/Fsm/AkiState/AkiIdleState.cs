using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [AkiState]
    public class AkiIdleState : MDDFsmState<Entity>
    {
        private ClipState.Transition idle;

        public override bool IsButtomState
        {
            get
            {
                return true;
            }
        }

        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);
            Fsm = fsm;
            Log.Info("创建akiIdle状态。");
            idle = fsm.Owner.CachedAnimContainer.GetAnimation("Idle");      
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            //base.OnEnter(fsm);
            Log.Info("进入akiIdle状态。");
            fsm.SetData<VarBoolean>(GetType().Name, false);
            fsm.Owner.CachedAnimancer.Play(idle);
        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁akiIdle状态。");
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开akiIdle状态。");
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
                Fsm.CurrentState.ChangeState(Fsm,GetType());
            }
            else
            {
                skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.STOP);
                Fsm.CurrentState.ChangeState(Fsm, GetType());
            }        
        }
    }
}

