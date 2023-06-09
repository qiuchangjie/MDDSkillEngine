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
            //动画通用流程
            if (anim == null)//如果没有对应的特化动画 则 播放通用动画
            {
                actor.CachedAnimancer.Play(actor.CachedAnimContainer.GetAnimation("Normal"));
            }
            else
            {
                if (actor.CachedAnimancer.IsPlaying(anim))
                {
                    actor.CachedAnimancer.Stop(anim);
                    actor.CachedAnimancer.Play(anim, 0);
                }
                else
                {
                    actor.CachedAnimancer.Play(anim);
                }
            }

                     
            SkillTimeline<Entity> skillTimeline1= skillTimeline as SkillTimeline<Entity>;
            Log.Info("{0}进入动画clip name：{1} currenttime:{2}",LogConst.SKillTimeline,skillData.AnimationName, skillTimeline1.currentTime);
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);

        }

        public override void Exit()
        {
            base.Exit();
        }

        
    }
}


