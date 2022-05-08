using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System;

namespace MDDSkillEngine
{
    [Title("使用技能节点", TitleAlignment = TitleAlignments.Centered)]
    public class NP_UseSkillAction : NP_ClassForAction
    {
        [LabelText("技能id")]
        public int SkillID;

        public override System.Action GetActionToBeDone()
        {
            this.Action = this.UseSkill;
            return this.Action;
        }

        public void UseSkill()
        {
            Debug.LogError($"使用技能:{SkillID}");
            Game.Skill.GetSkillSystem(((Entity)owner).Id).UseSkill(SkillID);
        }
    }
}


