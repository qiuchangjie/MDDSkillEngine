using MDDGameFramework;
using MDDGameFramework.Runtime;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Result = MDDGameFramework.Action.Result;
using Request = MDDGameFramework.Action.Request;

namespace MDDSkillEngine
{
    [Title("释放技能", TitleAlignment = TitleAlignments.Centered)]
    public class NP_ReleaseSkillAction : NP_ClassForAction
    {
        public int SkillId;

        public override Func<Request, Result> GetFun3ToBeDone()
        {
            this.Func3 = ReleaseSkill;
            return this.Func3;
        }

        private Result ReleaseSkill(Request request)
        {
            Entity entity = owner as Entity;

            if (entity == null)
            {
                Log.Error("{0}技能未关联实体{1}", LogConst.Skill, SkillId);
            }

            ISkillSystem skillSystem = Game.Skill.GetSkillSystem(entity.Id);

            if (request == Request.START)
            {
                skillSystem.ReleaseSkill(SkillId);
                return Result.PROGRESS;
            }
            else if (request == Request.UPDATE)
            {
                if (skillSystem.GetSkillReleaseResultType() == SkillReleaseResultType.STOP)
                {
                    skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.NONE);
                    return Result.FAILED;
                }

                if (skillSystem.GetSkillReleaseResultType() == SkillReleaseResultType.FAIL)
                {
                    skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.NONE);
                    return Result.FAILED;
                }

                if (skillSystem.GetSkillReleaseResultType() == SkillReleaseResultType.PROGRESS)
                {
                    return Result.PROGRESS;
                }

                if (skillSystem.GetSkillReleaseResultType() == SkillReleaseResultType.SUCCSE)
                {
                    skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.NONE);
                    return Result.SUCCESS;
                }
            }
            else if (request == Request.CANCEL)
            {
                skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.NONE);
                return Result.FAILED;
            }


            return Result.PROGRESS;
        }     
    }
}


