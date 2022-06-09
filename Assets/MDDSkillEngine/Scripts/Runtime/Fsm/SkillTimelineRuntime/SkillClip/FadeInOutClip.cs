using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class FadeInOutClip : SkillClip
    {
        FadeInOutSkillData skillData;

        public override void Init(SkillDataBase data, Entity actor, SkillTimeline skillTimeline)
        {
            base.Init(data, actor, skillTimeline);
            skillData = data as FadeInOutSkillData;
            this.skillTimeline = skillTimeline;
            this.actor = actor;
        }

        public override void Enter()
        {
            //控制状态是否可以中断
            if (skillData.type == FadeInOutType.In)
                skillTimeline.SetStateCantStop(true);
            else
                skillTimeline.SetStateCantStop(false);

            SkillTimeline<Entity> skillTimeline1 = skillTimeline as SkillTimeline<Entity>;
            Log.Info("{0}进入fadeclip name：{1} currenttime:{2}", LogConst.SKillTimeline, skillData.ResouceName, skillTimeline1.currentTime);
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);
            Log.Info("{0}upodatefadeClip name：{1}", LogConst.SKillTimeline, GetType().Name);
            duration += currentTime;

        }

        public override void Exit()
        {
            base.Exit();
            //Game.Entity.HideEntity(colid);
            Log.Info("{0}离开fadeClip name：{1}", LogConst.SKillTimeline, GetType().Name);
        }


    }
}


