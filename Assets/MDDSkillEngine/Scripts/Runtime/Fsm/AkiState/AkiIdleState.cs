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

        public override bool StrongState
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
            fsm.SetData<VarBoolean>("isMove",false);

            
           
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
            if (fsm.GetData<VarBoolean>("isMove"))
            {
                ChangeState<AkiRunState>(fsm);
            }

            //if (fsm.GetData<VarBoolean>("attack1"))
            //{
            //    ChangeState<AkiAttack1State>(fsm);
            //}

            //if (fsm.GetData<VarBoolean>("shunxi"))
            //{
            //    ChangeState<AkiShunXiState>(fsm);               
            //}

            //if (fsm.GetData<VarBoolean>("jianrenfengbao"))
            //{
            //    ChangeState<Akijianrenfengbao>(fsm);
            //}

            //if (fsm.GetData<VarBoolean>("skilldatatest"))
            //{
            //    ChangeState<AkiSkillDataTest>(fsm);
            //}
        }

        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
            VarBoolean varBoolean = (VarBoolean)newValue;

            if (varBoolean.Value == false)
                return;

            ISkillSystem skillSystem = Game.Skill.GetSkillSystem(Fsm.Owner.Id);

            if (skillSystem.GetSkillReleaseResultType() == SkillReleaseResultType.NONE)
            {
                Fsm.CurrentState.Finish(Fsm);
            }
            else
            {
                skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.STOP);
                Fsm.CurrentState.Finish(Fsm);
            }        
        }
    }
}

