using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class EntityClip : SkillClip
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
          
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);
            Log.Info("{0}upodateCDClip name：{1}", LogConst.SKillTimeline, GetType().Name);
        }

        public override void Exit()
        {
            base.Exit();
            Log.Info("{0}离开CD name：{1}", LogConst.SKillTimeline, GetType().Name);
        }


    }
}


