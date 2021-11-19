using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    [entiity]
    public sealed class GoOn : FsmState<entiity>
    {
        public float time;

        protected override void OnEnter(IFsm<entiity> fsm)
        {
            base.OnEnter(fsm);
            time = 9;
            Debug.LogError("进入goOn状态");
        }

        protected override void OnUpdate(IFsm<entiity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            time -= elapseSeconds;

            if (time <= 0)
            {
               // Finish<GoOn>(fsm);
            }

            Debug.LogError("正在GoOn状态");
        }

        
    }
}
