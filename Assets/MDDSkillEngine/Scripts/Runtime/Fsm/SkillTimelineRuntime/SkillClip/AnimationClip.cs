﻿using System;
using Animancer;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class AnimationClip : SkillClip
    {
        AnimationSkillData skillData;

        private ClipState.Transition anim;

        public override void Init(SkillDataBase data,Entity actor,SkillTimeline skillTimeline)
        {
            base.Init(data, actor, skillTimeline);
            skillData = data as AnimationSkillData;
            this.skillTimeline = skillTimeline;
            this.actor = actor;
            anim = actor.CachedAnimContainer.GetAnimation(skillData.AnimationName);
        }

        public override void Enter()
        {
            actor.CachedAnimancer.Play(anim);
            SkillTimeline<Player> skillTimeline1= skillTimeline as SkillTimeline<Player>;
            Log.Info("{0}进入动画clip name：{1} currenttime:{2}",LogConst.SKillTimeline,skillData.AnimationName, skillTimeline1.currentTime);
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);

        }

        public override void Exit()
        {
            
        }

        
    }
}

