using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [AkiState]
    public class AkiAttack1State : MDDFsmState<Entity>
    {
        /// <summary>
        ///  对于普攻动画的处理明显会有一些特殊
        ///  因为游戏中有攻击间隔的概念
        ///  所以需要在攻击间隔时间内播完一整个普攻动画
        ///   speed = 动画时长/普工间隔
        /// </summary>
        private ClipState.Transition attack;

        private float attackTime;

        private System.Action endAction;


       
        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建aki攻击状态。");

            attack = fsm.Owner.CachedAnimContainer.GetAnimation("Attack1");

            fsm.SetData<VarBoolean>("attack1",false);
            attackTime = attack.MaximumDuration;

            //Log.Error("attackAnimTime:{0}", attackTime);

            endAction += ()=> 
            {
                Log.Info("attack1结束事件");
                fsm.SetData<VarBoolean>("attack1", false); 
            };
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("进入aki攻击状态。");
            attack.Events.OnEnd += endAction;
            fsm.Owner.CachedAnimancer.Play(attack);
        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁aki攻击状态。");
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            attack.Events.OnEnd -= endAction;
            Log.Info("离开aki攻击状态。");
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            if (!fsm.GetData<VarBoolean>("attack1"))
            {
                Finish(fsm);
            }
        }

        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
           
        }
    }
}

