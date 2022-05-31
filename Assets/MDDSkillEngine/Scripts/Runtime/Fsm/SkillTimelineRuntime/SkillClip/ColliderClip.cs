using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class ColliderClip : SkillClip
    {
        ColliderSkillData skillData;


        public override void Init(SkillDataBase data, Entity actor, SkillTimeline skillTimeline)
        {
            base.Init(data, actor, skillTimeline);
            skillData = data as ColliderSkillData;
            this.skillTimeline = skillTimeline;
            this.actor = actor;
        }

        public override void Enter()
        {

            if (skillData.Speed == 0)
            {
                Game.Entity.ShowCollider(new ColliderData(Game.Entity.GenerateSerialId(), 20001, actor)
                {
                    localRotation = skillData.localRotation,
                    localScale = skillData.localScale,
                    localeftPostion = skillData.localeftPostion,
                    boundCenter = skillData.boundCenter,
                    boundSize = skillData.boundSize,
                    Duration = this.GetLength()
                });
            }
            else
            {
                Game.Entity.ShowCollider(typeof(NormalMoveCollider), skillData.ColliderName, new MoveColliderData(Game.Entity.GenerateSerialId(), 0, actor)
                {
                    Speed = skillData.Speed,
                    HitBuffDuration = skillData.BuffDuration,
                    HitForce = skillData.Force,
                    buffName = skillData.AddBuffName,
                    EffectID = skillData.Effectid,
                    localRotation = skillData.localRotation,
                    localScale = skillData.localScale,
                    localeftPostion = skillData.localeftPostion,
                    boundCenter = skillData.boundCenter,
                    boundSize = skillData.boundSize,
                    Duration = this.GetLength()
                });
            }
            SkillTimeline<Entity> skillTimeline1 = skillTimeline as SkillTimeline<Entity>;
            Log.Info("{0}进入动画clip na1111111111111111me：{1} currenttime:{2}", LogConst.SKillTimeline, skillData.ResouceName, skillTimeline1.currentTime);
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


