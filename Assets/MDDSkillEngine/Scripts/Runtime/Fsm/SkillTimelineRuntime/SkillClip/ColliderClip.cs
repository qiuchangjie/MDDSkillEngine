using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class ColliderClip : SkillClip
    {
        ColliderSkillData skillData;

        Entity col;

        public override void Init(SkillDataBase data, Entity actor,SkillTimeline skillTimeline)
        {
            base.Init(data, actor, skillTimeline);
            skillData = data as ColliderSkillData;
            this.skillTimeline = skillTimeline;
            this.actor = actor;
        }

        public override void Enter()
        {
            int id = Game.Entity.GenerateSerialId();

            Game.Entity.ShowCollider(new ColliderData(id, 20001, actor));
            col = Game.Entity.GetEntity(id).Logic as Entity;

            Game.Entity.AttachEntity(col.Id,actor.Id);

            BoxCollider Box = col.GetComponent<BoxCollider>();
            Box.size = skillData.boundSize;
            Box.center = skillData.boundCenter;

            col.CachedTransform.localRotation = skillData.localRotation;
            col.CachedTransform.localPosition = skillData.localeftPostion;
            col.CachedTransform.localScale = skillData.localScale;

            

            Log.Info("{0}进入ColliderClip name：{1},time:{2}", LogConst.SKillTimeline, GetType().Name, duration);
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);
            Log.Info("{0}upodateColliderClip name：{1}", LogConst.SKillTimeline, GetType().Name);
            duration += currentTime;

        }

        public override void Exit()
        {
            Game.Entity.HideEntity(col);
            Log.Info("{0}离开ColliderClip name：{1}", LogConst.SKillTimeline, GetType().Name);
        }


    }
}


