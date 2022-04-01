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
    public class Akijianrenfengbao : FsmState<Player>
    {
        public override bool CantStop
        {
            get
            {
                return true;
            }
        }

        private ClipState.Transition jianrenfengbao;

        private System.Action endAction;

        private EventHandler<GameEventArgs> colliderAction;

        private float duration = 5f;

        private float distance = 0;
        private float speed = 27;

        IFsm<Player> Fsm;

        protected override void OnInit(IFsm<Player> fsm)
        {
            base.OnInit(fsm);

            Log.Info("创建剑刃风暴状态。");

            //shunXi = fsm.Owner.CachedAnimContainer.GetAnimation("ShunXi");
            //shunXi2 = fsm.Owner.CachedAnimContainer.GetAnimation("ShunXi2");

            colliderAction = ColliderStayAction;
          
            fsm.SetData<VarBoolean>("jianrenfengbao", false);
            Fsm = fsm;
        }

        protected override void OnEnter(IFsm<Player> fsm)
        {
            duration = 5f;

            base.OnEnter(fsm);

            int effid;
            int colid;

            effid = Game.Entity.GenerateSerialId();
            colid = Game.Entity.GenerateSerialId();

            Game.Entity.ShowEffect(new EffectData(effid, 70005)
            {
                Position = fsm.Owner.CachedTransform.position,
                Rotation = fsm.Owner.CachedTransform.rotation,
                KeepTime = 5f
            });

            Game.Entity.ShowCollider(new ColliderData(colid, 20000, fsm.Owner)
            {
                Rotation = fsm.Owner.CachedTransform.rotation,
                Position = fsm.Owner.CachedTransform.position + new Vector3(0f, 0.5f, 0),
                LocalScale = new Vector3(1f, 1f, 1f),
                Duration = 5f
            });

            Game.Entity.AttachEntity(effid, fsm.Owner.Id);
            Game.Entity.AttachEntity(colid, fsm.Owner.Id);

            Game.Event.Subscribe(ColliderEnterEventArgs.EventId, colliderAction);         
        }

        protected override void OnDestroy(IFsm<Player> fsm)
        {
            base.OnDestroy(fsm);
            Log.Debug("销毁剑刃风暴状态。");
        }

        protected override void OnLeave(IFsm<Player> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Game.Event.Unsubscribe(ColliderEnterEventArgs.EventId, colliderAction);
            fsm.SetData<VarBoolean>("jianrenfengbao", false);
            Log.Debug("离开剑刃风暴。");
        }

        protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            duration -= elapseSeconds;
            if (duration <= 0)
            {
                Finish(fsm);             
            }
        }

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
                Game.Fsm.GetFsm<Enemy>(target.Id.ToString()).SetData<VarBoolean>("died", true);
                Game.Entity.HideEntity((Entity)sender);
                return;
            }

            //Game.Buff.AddBuff(target.Id.ToString(), "Dubao", col.Other, col.Owner);

            //Game.Fsm.GetFsm<Enemy>(target.Id.ToString()).SetData<VarBoolean>("damage", true);
        }

        private void ColliderAction(object sender, GameEventArgs e)
        {
            ColliderEnterEventArgs col = (ColliderEnterEventArgs)e;

            if (col.Owner != Fsm.Owner)
            {
                return;
            }

            TargetableObject target = col.Other as TargetableObject;

            Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70003)
            {
                Position = col.Other.CachedTransform.position,
                Rotation = col.Other.CachedTransform.rotation
            });

            int entityDamageHP = AIUtility.CalcDamageHP(200, 0);

            target.ApplyDamage(target, entityDamageHP);

            if (target.IsDead)
            {
                Game.Fsm.GetFsm<Enemy>(target.Id.ToString()).SetData<VarBoolean>("died", true);
                Game.Entity.HideEntity((Entity)sender);
                return;
            }

            Game.Buff.AddBuff(target.Id.ToString(), "Dubao", col.Other, col.Owner);

            Game.Fsm.GetFsm<Enemy>(target.Id.ToString()).SetData<VarBoolean>("damage", true);


            Game.Entity.HideEntity((Entity)sender);
        }
    }
}

