using MDDGameFramework.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using MDDGameFramework;

namespace MDDSkillEngine
{
    public static class SkillFactory
    {   
        public static Skill AcquireSkill(int SkillID,Entity Owner)
        {

            IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();
            DRSkill dRSkill = dtSkill.GetDataRow(SkillID);
            Skill skill =  Game.NPBehave.CreatBehaviourTree(dRSkill.AssetName, Owner) as Skill;

            return skill;
        }

    }
}
