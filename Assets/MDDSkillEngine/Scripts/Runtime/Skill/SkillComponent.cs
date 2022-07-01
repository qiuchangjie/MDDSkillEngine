using MDDGameFramework;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class SkillComponent : MDDGameFrameworkComponent
    {
        private Dictionary<int, ISkillSystem> skillSystemDic;

        protected override void Awake()
        {
            base.Awake();

            skillSystemDic = new Dictionary<int, ISkillSystem>();
        }

        public ISkillSystem CreateSkillSystem<T>(Entity Owner) where T : Entity
        {
            Player player = Owner as Player;
            if (player != null)
            {
                KealSkillSystem sys = KealSkillSystem.Create(player);
                skillSystemDic.Add(sys.Owner.Id, sys);
                return sys;
            }

            SkillSystem<T> skillSystem = SkillSystem<T>.Create(Owner);

            skillSystemDic.Add(skillSystem.Owner.Id,skillSystem);

            Log.Info("{1}创建技能系统成功 挂载实体:{0}", Owner.Id,LogConst.Skill);

            return skillSystem;
        }

        public ISkillSystem GetSkillSystem(int id)
        {
            ISkillSystem skillSystem;

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

        public void RemoveSkillSystem(int id)
        {
            ISkillSystem skillSystem;
            if (skillSystemDic.TryGetValue(id, out skillSystem))
            {
                skillSystemDic.Remove(id);
                skillSystem.Shutdown();
            }
        }    


    }
}
