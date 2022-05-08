using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System;

namespace MDDSkillEngine
{
    [Title("重置CD", TitleAlignment = TitleAlignments.Centered)]
    public class NP_ResetCDAction : NP_ClassForAction
    {
     
        public override System.Action GetActionToBeDone()
        {
            this.Action = this.RetCD;
            return this.Action;
        }

        public void RetCD()
        {
            Blackboard blackboard = BelongtoRuntimeTree.GetBlackboard();

            Debug.LogError($"cdtime:{blackboard.Get<VarFloat>("cdtime").Value}");

            blackboard.Set<VarFloat>("cd",blackboard.Get<VarFloat>("cdtime").Value);
        }
    }
}


