using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System;

namespace MDDSkillEngine
{
    [Title("打印信息", TitleAlignment = TitleAlignments.Centered)]
    public class NP_ProcessCDAction : NP_ClassForAction
    {

        public override System.Action GetActionToBeDone()
        {
            this.Action = this.ProcessCD;
            return this.Action;
        }

        public void ProcessCD()
        {
           float cd = BelongtoRuntimeTree.GetBlackboard().Get<VarFloat>("cd");
           cd -= Time.deltaTime;
           VarFloat var = ReferencePool.Acquire<VarFloat>();
           var.Value = cd;
           Debug.LogError($"cddlet:{cd}");
           BelongtoRuntimeTree.GetBlackboard().Set("cd", var);
        }
    }
}


