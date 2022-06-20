﻿using Animancer;
using DG.Tweening;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [Hero103]
    public class Hero103Spawn : MDDFsmState<Entity>
    {
       
        private ClipState.Transition Swap;

        private float ChangeTime;

        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);
            Log.Info("{0}创建{1}状态", LogConst.FSM, GetType().Name);
            Swap = fsm.Owner.CachedAnimContainer.GetAnimation("103_m_a_spawn");
            Fsm = fsm;
            ChangeTime = Swap.Clip.length;
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            base.OnEnter(fsm);
            //动画
            Log.Info("{0}进入{1}状态", LogConst.FSM, GetType().Name);
            fsm.Owner.CachedAnimancer.Play(Swap);
        }


        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("{0}摧毁{1}状态", LogConst.FSM, GetType().Name);
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("{0}离开{1}状态", LogConst.FSM, GetType().Name);
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            if (duration >= ChangeTime)
            {
                fsm.SetData<VarBoolean>(typeof(Hero103Idle).Name, true);
            }
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

