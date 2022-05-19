using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [AkiState]
    public class AkiIdleState : MDDFsmState<Player>
    {
        private ClipState.Transition idle;

        IFsm<Player> Fsm;

        public override bool StrongState
        {
            get
            {
                return true;
            }
        }

        protected override void OnInit(IFsm<Player> fsm)
        {
            base.OnInit(fsm);
            Fsm = fsm;
            Log.Info("创建akiIdle状态。");
            idle = fsm.Owner.CachedAnimContainer.GetAnimation("Idle");
            fsm.SetData<VarBoolean>("isMove",false);

            //添加该状态是否激活的观察者
            fsm.AddObserver(GetType().Name, Observing);
        }

        protected override void OnEnter(IFsm<Player> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("进入akiIdle状态。");
            fsm.SetData<VarBoolean>(GetType().Name, false);
            fsm.Owner.CachedAnimancer.Play(idle);
        }

        protected override void OnDestroy(IFsm<Player> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁akiIdle状态。");
        }

        protected override void OnLeave(IFsm<Player> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开akiIdle状态。");
        }

        protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
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

        private void Observing(Blackboard.Type type, Variable newValue)
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

