using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class EffectClip : SkillClip
    {
        EffectSkillData skillData;


        public override void Init(SkillDataBase data, Entity actor, SkillTimeline skillTimeline)
        {
            base.Init(data, actor, skillTimeline);
            skillData = data as EffectSkillData;
            this.skillTimeline = skillTimeline;
            this.actor = actor;
        }

        public override void Enter()
        {
            int id = Game.Entity.GenerateSerialId();

            Game.Entity.ShowEffect(new EffectData(id, 70006)
            {
                //Position = actor.CachedTransform.position + skillData.localeftPostion,
                //Rotation = actor.CachedTransform.rotation,
                //LocalScale = skillData.localScale,
            });

            Entity effect = Game.Entity.GetEntity(id).Logic as Entity;

            Game.Entity.AttachEntity(effect.Id,actor.Id);

            float angle;
            Vector3 vector3;
            skillData.localRotation.ToAngleAxis(out angle, out vector3);
            effect.CachedTransform.localRotation = skillData.localRotation;
            effect.CachedTransform.localPosition = skillData.localeftPostion;
            effect.CachedTransform.localScale = skillData.localScale;

           Log.Info("{0}进入effectclip name：{1}", LogConst.SKillTimeline, "1");
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


