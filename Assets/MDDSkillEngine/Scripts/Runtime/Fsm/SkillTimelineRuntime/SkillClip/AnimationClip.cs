using System;
using Animancer;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class AnimationClip : SkillClip
    {
        AnimationSkillData skillData;

        private ClipState.Transition anim;

        public override void Init(SkillDataBase data,Entity actor)
        {
            skillData = data as AnimationSkillData;

            this.actor = actor;
            anim = actor.CachedAnimContainer.GetAnimation(skillData.AnimationName);
        }

        public override void Enter()
        {
            actor.CachedAnimancer.Play(anim);
            Log.Info("{0}进入动画clip name：{1}",LogConst.SKillTimeline,skillData.AnimationName);
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


