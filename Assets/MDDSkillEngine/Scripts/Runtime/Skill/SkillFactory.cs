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
        /// <summary>
        /// 通过技能返回实例化的技能
        /// </summary>
        /// <param name="SkillID"></param>
        /// <param name="Owner"></param>
        /// <returns></returns>
        public static Skill AcquireSkill(int SkillID,Entity Owner)
        {

            IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();
            DRSkill dRSkill = dtSkill.GetDataRow(SkillID);

            if (dRSkill == null)
            {
                Log.Error("{0}尝试装配不存在的技能id:{1}",LogConst.Skill, SkillID);
            }

            //技能行为树装配
            Skill skill =  Game.NPBehave.CreatBehaviourTree(dRSkill.AssetName, Owner) as Skill;

            return skill;
        }

    }
}
