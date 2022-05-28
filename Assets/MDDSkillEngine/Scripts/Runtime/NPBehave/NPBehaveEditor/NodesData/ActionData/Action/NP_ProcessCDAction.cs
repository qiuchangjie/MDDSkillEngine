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
           Variable cd = BelongtoRuntimeTree.GetBlackboard().Get("cd");
           VarFloat varFloat = cd as VarFloat;
           BelongtoRuntimeTree.GetBlackboard().Set<VarFloat>("cd", varFloat.Value -= Time.deltaTime);     
        }
    }
}


