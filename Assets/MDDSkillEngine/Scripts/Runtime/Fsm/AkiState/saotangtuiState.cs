using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [AkiState]
    public class saotangtuiState : SkillTimelineState<Player>
    {
        IFsm<Player> Fsm;

        protected override void OnInit(IFsm<Player> fsm)
        {
            base.OnInit(fsm);
            Fsm = fsm;

            //添加该状态是否激活的观察者
            fsm.AddObserver(GetType().Name,Observing);
        }

        protected override void OnEnter(IFsm<Player> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("{0}进入{1}状态",LogConst.FSM,GetType().Name);
        }

        protected override void OnDestroy(IFsm<Player> fsm)
        {
            base.OnDestroy(fsm);
        }

        protected override void OnLeave(IFsm<Player> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        }

        private void Observing(Blackboard.Type type, Variable newValue)
        {
            VarBoolean varBoolean = (VarBoolean)newValue;

            if (varBoolean.Value == false)
                return;

            if (Fsm.GetCurrStateName() == "AkiIdleState")
            {
                ChangeState<saotangtuiState>(Fsm);
            }
        }
    }
}

