using System;
using System.Collections.Generic;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class ColliderClip : SkillClip
    {
        ColliderSkillData skillData;

        int id;

        List<Vector3> bezierPath = new List<Vector3>();

        public override void Init(SkillDataBase data, Entity actor, SkillTimeline skillTimeline)
        {
            base.Init(data, actor, skillTimeline);
            skillData = data as ColliderSkillData;
            this.skillTimeline = skillTimeline;
            this.actor = actor;
        }

        public override void Enter()
        {
            id = Game.Entity.GenerateSerialId();

            if (skillData.Speed == 0)
            {
                Game.Entity.ShowCollider(typeof(MDDCollider), skillData.ColliderName, new ColliderData(id, 0, actor)
                {
                    targetType = skillTimeline.TargetType,
                    HitEffectName = skillData.EffectName,
                    buffName = skillData.AddBuffName,
                    localRotation = skillData.localRotation,
                    localScale = skillData.localScale,
                    localeftPostion = skillData.localeftPostion,
                    boundCenter = skillData.boundCenter,
                    boundSize = skillData.boundSize,
                    Duration = this.GetLength(),
                    hasPath = skillData.hasPath,
                    useSpeed = skillData.useSpeed,
                    bezierPath = skillData.bezierPath,
                    bezierPathLength = skillData.bezierPathLength,
                    bezierPathParentPosition = skillData.bezierPathParentPosition,
                    bezierPathParentRotation = skillData.bezierPathParentRotation,
                }) ;
            }
            else
            {
                Game.Entity.ShowCollider(typeof(NormalMoveCollider), skillData.ColliderName, new MoveColliderData(id, 0, actor)
                {
                    targetType = skillTimeline.TargetType,
                    Speed = skillData.Speed,
                    HitBuffDuration = skillData.BuffDuration,
                    HitForce = skillData.Force,
                    buffName = skillData.AddBuffName,
                    HitEffectName = skillData.EffectName,
                    localRotation = skillData.localRotation,
                    localScale = skillData.localScale,
                    localeftPostion = skillData.localeftPostion,
                    boundCenter = skillData.boundCenter,
                    boundSize = skillData.boundSize,
                    Duration = this.GetLength(),
                    hasPath = skillData.hasPath,
                    useSpeed = skillData.useSpeed,
                    bezierPath = skillData.bezierPath,
                    bezierPathLength = skillData.bezierPathLength,
                    bezierPathParentPosition = skillData.bezierPathParentPosition,
                    bezierPathParentRotation = skillData.bezierPathParentRotation,
                });
            }

            bezierPath.Clear();

            //坐标转换 将曲线本地坐标转换为世界坐标
            for (int i = 0; i < skillData.bezierPath.Length; i++)
            {
                Vector3 vec3;
                vec3 = actor.CachedTransform.TransformPoint(skillData.bezierPath[i]);
                bezierPath.Add(vec3);
            }

            SkillTimeline<Entity> skillTimeline1 = skillTimeline as SkillTimeline<Entity>;
            Log.Info("{0}进入Colliderclip ：{1} currenttime:{2}", LogConst.SKillTimeline, skillData.ResouceName, skillTimeline1.currentTime);
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);
           // Log.Info("{0}upodateColliderClip name：{1}", LogConst.SKillTimeline, GetType().Name);
            duration += currentTime;

            //利用贝塞尔曲线
            if (!skillData.useSpeed)
            {
                if (skillData.hasPath)
                {
                    if (Game.Entity.HasEntity(id))
                    {
                        Entity entity = Game.Entity.GetGameEntity(id);
                        entity.transform.position = AIUtility.GetPoint(currentTime / this.GetLength(), skillData.bezierPathLength, bezierPath);
                    }
                }
            }         
        }

        public override void Exit()
        {
            //Game.Entity.HideEntity(colid);
            base.Exit();
            Log.Info("{0}离开ColliderClip name：{1}", LogConst.SKillTimeline, GetType().Name);
        }


    }
}


