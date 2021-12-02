using MDDGameFramework;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class SkillComponent : MDDGameFrameworkComponent
    {
        private Dictionary<int, SkillSystem> skillSystemDic;

        protected override void Awake()
        {
            base.Awake();
            skillSystemDic = new Dictionary<int, SkillSystem>();
        }

        public ISkillSystem CreateSkillSystem(Entity Owner)
        {
            SkillSystem skillSystem = SkillSystem.Create(Owner);
            skillSystemDic.Add(skillSystem.Owner.Id,skillSystem);

            return skillSystem;
        }

        public ISkillSystem GetSkillSystem(int id)
        {
            SkillSystem skillSystem;

            if (skillSystemDic.TryGetValue(id, out skillSystem))
            {
                return skillSystem;
            }

            if (skillSystem == null)
            {
                Log.Error("实体:{0}不存在技能系统",id);
            }

            return null;
        }




    }
}
