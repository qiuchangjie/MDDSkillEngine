using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class ColliderClip : SkillClip
    {
        ColliderSkillData skillData;


        public override void Init(SkillDataBase data, Entity actor,SkillTimeline skillTimeline)
        {
            base.Init(data, actor, skillTimeline);
            skillData = data as ColliderSkillData;
            this.skillTimeline = skillTimeline;
            this.actor = actor;
        }

        public override void Enter()
        {
            //skillTimeline.SetStateCantStop(true);

            //ISkillSystem skillSystem = Game.Skill.GetSkillSystem(actor.Id);
            //skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.SUCCSE);

            int colid = Game.Entity.GenerateSerialId();

            Game.Entity.ShowCollider(new ColliderData(colid, 20001, actor)
            {
                localRotation = skillData.localRotation,
                localScale = skillData.localScale,
                localeftPostion = skillData.localeftPostion,
                boundCenter = skillData.boundCenter,
                boundSize = skillData.boundSize,
                Duration = this.GetLength()
            });
            //col = Game.Entity.GetEntity(id).Logic as Entity;

            //Game.Entity.AttachEntity(col.Id, actor.Id);

            //BoxCollider Box = col.GetComponent<BoxCollider>();
            //Box.size = skillData.boundSize;
            //Box.center = skillData.boundCenter;

            //col.CachedTransform.localRotation = skillData.localRotation;
            //col.CachedTransform.localPosition = skillData.localeftPostion;
            //col.CachedTransform.localScale = skillData.localScale;



            SkillTimeline<Entity> skillTimeline1 = skillTimeline as SkillTimeline<Entity>;
            Log.Info("{0}进入动画clip name：{1} currenttime:{2}", LogConst.SKillTimeline, skillData.ResouceName, skillTimeline1.currentTime);
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);
            Log.Info("{0}upodateColliderClip name：{1}", LogConst.SKillTimeline, GetType().Name);
            duration += currentTime;

        }

        public override void Exit()
        {
            //Game.Entity.HideEntity(colid);
            Log.Info("{0}离开ColliderClip name：{1}", LogConst.SKillTimeline, GetType().Name);
        }


    }
}


