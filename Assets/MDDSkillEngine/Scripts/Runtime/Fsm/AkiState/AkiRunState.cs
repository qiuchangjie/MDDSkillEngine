using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [AkiState]
    public class AkiRunState : MDDFsmState<Entity>
    {
        private ClipState.Transition Run;

        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建akiRun状态。");
            Run = fsm.Owner.CachedAnimContainer.GetAnimation("Run");
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("进入akiRun状态。");
            fsm.Owner.CachedAnimancer.Play(Run);            
        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁akiRun状态。");
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开akiRun状态。");
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            if (!fsm.GetData<VarBoolean>("isMove"))
            {
                //ChangeState<AkiIdleState>(fsm);
                Finish(fsm);
            }

            if (fsm.GetData<VarBoolean>("attack1"))
            {
                ChangeState<AkiAttack1State>(fsm);
            }

        }
    }
}

