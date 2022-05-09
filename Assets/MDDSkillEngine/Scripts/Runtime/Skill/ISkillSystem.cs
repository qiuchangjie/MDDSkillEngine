using MDDGameFramework;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public interface ISkillSystem
    {
        void AddSkill(int id);

        Skill GetSkill(int id);

        void UseSkill(int id);

        void ReleaseSkill(int id);

        bool UpgradeSkill(int id);

    }
}
