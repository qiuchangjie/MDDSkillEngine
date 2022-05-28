using MDDGameFramework;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public interface ISkillSystem
    {
        void AddSkill(int skillid);

        void AddSkill(int skillid, int index);

        Skill GetSkill(int skillid);

        void UseSkill(int skillid);

        void ReleaseSkill(int skillid);

        bool UpgradeSkill(int skillid);

        void RemoveSkill(int skillid);

        Blackboard GetSkillBlackboard(int skillid);

        Blackboard GetPubBlackboard();

        void SetBlackboard(Blackboard blackboard);

        SkillReleaseResultType GetSkillReleaseResultType();

        void SetSkillReleaseResultType(SkillReleaseResultType releaseResultType);
    }
}
