using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class EffectClip : SkillClip
    {
        EffectSkillData skillData;
        int id;

        public override void Init(SkillDataBase data, Entity actor, SkillTimeline skillTimeline)
        {
            base.Init(data, actor, skillTimeline);
            skillData = data as EffectSkillData;
            this.skillTimeline = skillTimeline;
            this.actor = actor;
        }

        public override void Enter()
        {
            id = Game.Entity.GenerateSerialId();

            Game.Entity.ShowEffect(typeof(Effect),skillData.ResouceName,new EffectData(id, 70006)
            {
                Owner = actor,
                KeepTime = this.GetLength(),
                localeftPostion=skillData.localeftPostion,
                localRotation=skillData.localRotation,
                localScale=skillData.localScale,
                //hasPath=skillData.hasPath,
                //bezierPath=skillData.bezierPath,
                //bezierPathLength=skillData.bezierPathLength,
                //bezierPathParentPosition=skillData.bezierPathParentPosition,
                //bezierPathParentRotation=skillData.bezierPathParentRotation,              
            });

            //Entity effect = Game.Entity.GetEntity(id).Logic as Entity;

            //Game.Entity.AttachEntity(effect.Id,actor.Id);

            //effect.CachedTransform.localRotation = skillData.localRotation;
            //effect.CachedTransform.localPosition = skillData.localeftPostion;
            //effect.CachedTransform.localScale = skillData.localScale;

           Log.Info("{0}进入effectclip name：{1}", LogConst.SKillTimeline, "1");
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);

            if (skillData.hasPath)
            {
                if (Game.Entity.HasEntity(id))
                {
                   Entity entity = Game.Entity.GetGameEntity(id);
                   entity.transform.position = AIUtility.GetPoint(currentTime/this.GetLength(), skillData.bezierPathLength, skillData.bezierPath);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}


