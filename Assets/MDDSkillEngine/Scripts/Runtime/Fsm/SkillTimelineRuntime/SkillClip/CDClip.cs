using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class CDClip : SkillClip
    {
        CDSkillData skillData;

        public override void Init(SkillDataBase data, Entity actor, SkillTimeline skillTimeline)
        {
            base.Init(data, actor, skillTimeline);
            skillData = data as CDSkillData;
            this.skillTimeline = skillTimeline;
            this.actor = actor;
        }

        public override void Enter()
        {
            //将技能状态设置为成功 
            ISkillSystem skillSystem = Game.Skill.GetSkillSystem(actor.Id);
            skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.SUCCSE);

          
            SkillTimeline<Entity> skillTimeline1 = skillTimeline as SkillTimeline<Entity>;
            Log.Info("{0}进入CDclip name：{1} currenttime:{2}", LogConst.SKillTimeline, GetType().Name, skillTimeline1.currentTime);
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);
            Log.Info("{0}upodateCDClip name：{1}", LogConst.SKillTimeline, GetType().Name);
            duration += currentTime;

        }

        public override void Exit()
        {
            Log.Info("{0}离开CD name：{1}", LogConst.SKillTimeline, GetType().Name);
        }


    }
}


