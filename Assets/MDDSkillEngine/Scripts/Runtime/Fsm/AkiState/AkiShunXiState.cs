using Animancer;
using DG.Tweening;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [AkiState]
    public class AkiShunXiState : FsmState<Player>
    {
        private ClipState.Transition shunXi;
        private ClipState.Transition shunXi2;

        private System.Action endAction;

        private float duration;

        private float distance=0;
        private float speed = 27;

        protected override void OnInit(IFsm<Player> fsm)
        {
            base.OnInit(fsm);

            Log.Info("创建aki瞬袭状态。");

            shunXi = fsm.Owner.CachedAnimContainer.GetAnimation("ShunXi");
            shunXi2 = fsm.Owner.CachedAnimContainer.GetAnimation("ShunXi2");

            endAction += () => 
            { 
                Finish(fsm);
                fsm.SetData<VarBoolean>("shunxi", false);
                Log.Debug("瞬息结束");
            };
            
            fsm.SetData<VarBoolean>("shunxi", false);
        }

        protected override void OnEnter(IFsm<Player> fsm)
        {
            base.OnInit(fsm);

            shunXi2.Events.OnEnd += endAction;

            fsm.Owner.CachedAnimancer.Play(shunXi);

            fsm.Owner.CachedTransform.LookAt(Game.Select.currentClick);

            Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70001) 
            { 
                Position = fsm.Owner.CachedTransform.position,
                Rotation = fsm.Owner.CachedTransform.rotation
            });


            Log.Info("进入aki瞬袭状态");
        }

        protected override void OnDestroy(IFsm<Player> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁aki瞬袭状态。");
        }

        protected override void OnLeave(IFsm<Player> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            shunXi2.Events.OnEnd -= endAction;         
            distance = 0;
            Log.Info("离开aki瞬袭状态。");
        }

        protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
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
                    Vector3.MoveTowards(fsm.Owner.CachedTransform.position, Game.Select.currentClick, speed * elapseSeconds);
            }          
        }        
    }
}

