using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class EffectClip : SkillClip
    {
        EffectSkillData skillData;


        public override void Init(SkillDataBase data, Entity actor)
        {
            skillData = data as EffectSkillData;

            this.actor = actor;
        }

        public override void Enter()
        {
            int id = Game.Entity.GenerateSerialId();

            Game.Entity.ShowEffect(new EffectData(id, 70006)
            {
                Position = actor.CachedTransform.position + skillData.localeftPostion,
                Rotation = actor.CachedTransform.rotation,
                LocalScale = skillData.localScale,
            });

            Entity effect = Game.Entity.GetEntity(id).Logic as Entity;

            float angle;
            Vector3 vector3;
            skillData.localRotation.ToAngleAxis(out angle, out vector3);
            effect.CachedTransform.rotation = Quaternion.Euler(vector3);

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


