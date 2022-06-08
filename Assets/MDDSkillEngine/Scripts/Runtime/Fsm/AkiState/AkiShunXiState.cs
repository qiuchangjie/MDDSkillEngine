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
    public class AkiShunXiState : MDDFsmState<Entity>
    {
        public override bool CantStop
        {
            get
            {
                return true;
            }
        }

        private ClipState.Transition shunXi;
        private ClipState.Transition shunXi2;

        private System.Action endAction;
        private EventHandler<GameEventArgs> colliderAction;

        private float duration;

        private float distance=0;
        private float speed = 27;

        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);

            Log.Info("创建aki瞬袭状态。");

            shunXi = fsm.Owner.CachedAnimContainer.GetAnimation("ShunXi");
            shunXi2 = fsm.Owner.CachedAnimContainer.GetAnimation("Attack4");

            colliderAction = ColliderAction;

            endAction += () => 
            {
                Log.Debug("瞬息结束");
                fsm.SetData<VarBoolean>("shunxi", false);
                Finish(fsm);                                
            };
            
            fsm.SetData<VarBoolean>("shunxi", false);
            Fsm = fsm;
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
           
            base.OnEnter(fsm);

            Game.Event.Subscribe(ColliderEnterEventArgs.EventId, colliderAction);

            shunXi2.Events.OnEnd += endAction;

            fsm.Owner.CachedAnimancer.Play(shunXi);

            fsm.Owner.CachedTransform.LookAt(Game.Select.currentMouse);

            Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70001) 
            { 
                Position = fsm.Owner.CachedTransform.position,
                Rotation = fsm.Owner.CachedTransform.rotation
            });

            Game.Entity.ShowCollider(new ColliderData(Game.Entity.GenerateSerialId(), 20000, fsm.Owner)
            {
                Rotation = fsm.Owner.CachedTransform.rotation,
                Position = fsm.Owner.CachedTransform.position + fsm.Owner.CachedTransform.forward * 2 + new Vector3(0f,0.5f,0),
                LocalScale = new Vector3(1f, 1f, 4f)
            });

          
        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
            Log.Debug("销毁aki瞬袭状态。");
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);

            shunXi2.Events.OnEnd -= endAction;

            Game.Event.Unsubscribe(ColliderEnterEventArgs.EventId, colliderAction);

            distance = 0;
            Log.Debug("离开aki瞬袭状态。");
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            if (distance >= 5f)
            {
                distance = 0f;
                fsm.Owner.CachedAnimancer.Play(shunXi2);
                fsm.SetData<VarBoolean>("shunxi", false);
                Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70002) 
                { 
                    Position = fsm.Owner.CachedTransform.position ,
                    Rotation = fsm.Owner.CachedTransform.rotation
                });              
            }

            if (fsm.GetData<VarBoolean>("shunxi"))
            {
                distance += speed * elapseSeconds;

                fsm.Owner.CachedTransform.position =
                    Vector3.MoveTowards(fsm.Owner.CachedTransform.position, Game.Select.currentMouse, speed * elapseSeconds);
            }          
        }

        private void ColliderAction(object sender,GameEventArgs e)
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
                Game.Fsm.GetFsm<Entity>(target.Id.ToString()).SetData<VarBoolean>("died", true);
                Game.Entity.HideEntity((Entity)sender);
                return;
            }

            Game.Buff.AddBuff(target.Id.ToString(), "Dubao", col.Other, col.Owner);

            Game.Fsm.GetFsm<Entity>(target.Id.ToString()).SetData<VarBoolean>("damage", true);


            Game.Entity.HideEntity((Entity)sender);
        }

        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
            
        }
    }
}

