using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [AkiState]
    public class AkiAttack1State : FsmState<Player>
    {
        private ClipState.Transition attack;
     
        protected override void OnInit(IFsm<Player> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建aki攻击状态。");
            attack = fsm.Owner.CachedAnimContainer.GetAnimation("Attack1");
        }

        protected override void OnEnter(IFsm<Player> fsm)
        {
            base.OnInit(fsm);
            Log.Info("进入aki攻击状态。");
            fsm.Owner.CachedAnimancer.Play(attack);
        }

        protected override void OnDestroy(IFsm<Player> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁aki攻击状态。");
        }

        protected override void OnLeave(IFsm<Player> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开aki攻击状态。");
        }

        protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Log.Info("轮询aki攻击状态。");
        }
    }
}

