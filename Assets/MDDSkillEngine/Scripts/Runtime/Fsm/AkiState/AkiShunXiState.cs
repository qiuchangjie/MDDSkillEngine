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

        private System.Action endAction;

        protected override void OnInit(IFsm<Player> fsm)
        {
            base.OnInit(fsm);

            Log.Info("创建aki瞬袭状态。");

            shunXi = fsm.Owner.CachedAnimContainer.GetAnimation("ShunXi");

            fsm.SetData<VarBoolean>("shunxi", false);

        }

        protected override void OnEnter(IFsm<Player> fsm)
        {
            base.OnInit(fsm);

            fsm.Owner.CachedAnimancer.Play(shunXi);

            Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70001) { Position = fsm.Owner.CachedTransform.position });

            Tweener tweener = fsm.Owner.CachedTransform.DOMove(new Vector3(5, 0, 5), 1);

            tweener.SetEase(Ease.Linear);

            tweener.onComplete = delegate ()
            {
                fsm.SetData<VarBoolean>("shunxi", false);
                Debug.Log("移动完毕事件");
                Finish(fsm);
            };


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
            Log.Info("离开aki瞬袭状态。");
        }

        protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
           
        }


    }
}

