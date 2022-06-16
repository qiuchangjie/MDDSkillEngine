using Animancer;
using DG.Tweening;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [AkiState]
    public class Akijianrenfengbao : MDDFsmState<Entity>
    {
        public override bool CantStop
        {
            get
            {
                return true;
            }
        }

        private ClipState.Transition jianrenfengbao;

        private EventHandler<GameEventArgs> colliderAction;


        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建剑刃风暴状态。");
            jianrenfengbao = fsm.Owner.CachedAnimContainer.GetAnimation("jianrenfengbao");
            colliderAction = ColliderStayAction;
          
            Fsm = fsm;
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            duration = 5f;

            base.OnEnter(fsm);

            int effid;
            int colid;

            effid = Game.Entity.GenerateSerialId();
            colid = Game.Entity.GenerateSerialId();

            //动画
            fsm.Owner.CachedAnimancer.Play(jianrenfengbao);

            //特效
            Game.Entity.ShowEffect(new EffectData(effid, 70005)
            {
                Position = fsm.Owner.CachedTransform.position,
                Rotation = fsm.Owner.CachedTransform.rotation,
                KeepTime = 5f
            });

            //碰撞体
            Game.Entity.ShowCollider(new ColliderData(colid, 20000, fsm.Owner)
            {
                Rotation = fsm.Owner.CachedTransform.rotation,
                Position = fsm.Owner.CachedTransform.position + new Vector3(0f, 0.5f, 0),
                LocalScale = new Vector3(1f, 1f, 1f),
                Duration = 5f
            });

            Game.Event.Subscribe(ColliderEnterEventArgs.EventId, colliderAction);         
        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
            Log.Debug("销毁剑刃风暴状态。");
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Game.Event.Unsubscribe(ColliderEnterEventArgs.EventId, colliderAction);
            fsm.SetData<VarBoolean>("jianrenfengbao", false);
            Log.Debug("离开剑刃风暴。");
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            if (duration >= 5f)
            {
                Finish(fsm);             
            }
        }

        //碰撞事件
        private void ColliderStayAction(object sender, GameEventArgs e)
        {
            ColliderEnterEventArgs col = (ColliderEnterEventArgs)e;

            if (col.Owner != Fsm.Owner)
            {
                return;
            }

            int entityDamageHP = AIUtility.CalcDamageHP(5, 0);

            TargetableObject target = col.Other as TargetableObject;

            target.ApplyDamage(target, entityDamageHP);

            if (target.IsDead)
            {
                Game.Fsm.GetFsm<Entity>(target.Id.ToString()).SetData<VarBoolean>("died", true);
                Game.Entity.HideEntity((Entity)sender);
                return;
            }

            Game.Buff.AddBuff(target.Id.ToString(), "Dubao", col.Other, col.Owner);

            Game.Fsm.GetFsm<Entity>(target.Id.ToString()).SetData<VarBoolean>("damage", true);
        }


        /// <summary>
        /// 状态跳转
        /// 基于黑板的观察函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="newValue"></param>
        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
            VarBoolean varBoolean = (VarBoolean)newValue;

            if (varBoolean.Value == false)
                return;

            //可以根据需求自定跳转条件
            ChangeState(Fsm, GetType());
        }

    }
}

