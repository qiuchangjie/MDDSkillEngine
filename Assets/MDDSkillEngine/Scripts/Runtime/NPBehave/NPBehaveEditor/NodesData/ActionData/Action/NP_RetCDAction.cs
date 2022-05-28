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
    public class NP_RetCDAction : NP_ClassForAction
    {
     
        public override System.Action GetActionToBeDone()
        {
            this.Action = this.RetCD;
            return this.Action;
        }

        public void RetCD()
        {
            Blackboard blackboard = BelongtoRuntimeTree.GetBlackboard();

            Debug.LogError($"cdtime:{((blackboard.Get("cdtime")) as VarFloat).Value}");

            blackboard.Set("cd", blackboard.Get("cdtime").VDeepCopy());
        }
    }
}


