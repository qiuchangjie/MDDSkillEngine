using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class ColliderClip : SkillClip
    {
        ColliderSkillData skillData;

        public override void Init(SkillDataBase data, Entity actor)
        {
            skillData = data as ColliderSkillData;

            this.actor = actor;
        }

        public override void Enter()
        {
            int id = Game.Entity.GenerateSerialId();

            Game.Entity.ShowCollider(new ColliderData(id, 20001, actor)
            {
                Position = actor.CachedTransform.position + skillData.localeftPostion,
                Rotation = actor.CachedTransform.rotation,
                LocalScale = skillData.localScale,
            });

            Entity col = Game.Entity.GetEntity(id).Logic as Entity;

            float angle;
            Vector3 vector3;
            skillData.localRotation.ToAngleAxis(out angle, out vector3);
            col.CachedTransform.rotation = Quaternion.Euler(vector3);

            BoxCollider Box = col.GetComponent<BoxCollider>();
            Box.size = skillData.boundSize;
            Box.center = skillData.boundCenter;

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


