using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [AkiState]
    public class AkiRunState : FsmState<Player>
    {
        private ClipState.Transition Run;

        protected override void OnInit(IFsm<Player> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建akiRun状态。");
            Run = fsm.Owner.CachedAnimContainer.GetAnimation("Run");
        }

        protected override void OnEnter(IFsm<Player> fsm)
        {
            base.OnInit(fsm);
            Log.Info("进入akiRun状态。");
            fsm.Owner.CachedAnimancer.Play(Run);
        }

        protected override void OnDestroy(IFsm<Player> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁akiRun状态。");
        }

        protected override void OnLeave(IFsm<Player> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开akiRun状态。");
        }

        protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Log.Info("轮询akiRun状态。");
        }
    }
}

