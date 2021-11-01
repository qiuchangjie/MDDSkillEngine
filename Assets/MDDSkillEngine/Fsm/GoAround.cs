using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;

namespace MDDSkillEngine
{
    [entiity]
    public sealed class GoAround : FsmState<entiity>
    {
        public override bool StrongState
        {
            get { return true; }
        }

        protected override void OnEnter(IFsm<entiity> fsm)
        {
            base.OnEnter(fsm);

            Debug.LogError("进入GoAround");
        }

        protected override void OnUpdate(IFsm<entiity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            fsm.Owner.transform.position += new Vector3(1,0,0);

            Debug.LogError("12132131321");
        }
    }
}

