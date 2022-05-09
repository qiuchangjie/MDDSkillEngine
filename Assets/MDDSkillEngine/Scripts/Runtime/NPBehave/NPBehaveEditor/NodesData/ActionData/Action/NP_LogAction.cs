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
    public class NP_LogAction : NP_ClassForAction
    {
        [LabelText("信息")]
        public string LogInfo="shunxishunxi";

        public override System.Action GetActionToBeDone()
        {
            this.Action = this.TestLog;
            return this.Action;
        }

        public void TestLog()
        {
            Log.Info(LogInfo);
        }
    }
}


