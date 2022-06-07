using System;
using Animancer;
using MDDGameFramework.Runtime;
using UnityEngine;
using System.Collections.Generic;

namespace MDDSkillEngine
{
    public class EffectClip : SkillClip
    {
        EffectSkillData skillData;
        int id;

        List<Vector3> bezierPath=new List<Vector3>();

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

            bezierPath.Clear();

            //坐标转换 将曲线本地坐标转换为世界坐标
            for (int i = 0; i < skillData.bezierPath.Length; i++)
            {
                Vector3 vec3;
                vec3 = actor.CachedTransform.TransformPoint(skillData.bezierPath[i]);
                bezierPath.Add(vec3);
            }

           
           Log.Info("{0}进入effectclip name：{1}", LogConst.SKillTimeline, "1");
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);

            //利用贝塞尔曲线
            if (skillData.hasPath)
            {
                if (Game.Entity.HasEntity(id))
                {
                   Entity entity = Game.Entity.GetGameEntity(id);                 
                   entity.transform.position = AIUtility.GetPoint(currentTime/this.GetLength(), skillData.bezierPathLength, bezierPath);
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
        }


    }
}


