using System;
using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public class EntityClip : SkillClip
    {
        EntitySkillData skillData;

        int id = 2000;

        public override void Init(SkillDataBase data, Entity actor, SkillTimeline skillTimeline)
        {
            base.Init(data, actor, skillTimeline);
            skillData = data as EntitySkillData;
            this.skillTimeline = skillTimeline;
            this.actor = actor;
        }


        public override void Enter()
        {
            Game.Entity.ShowEntity(Utility.Assembly.GetType(Utility.Text.Format("MDDSkillEngine.{0}", skillData.EntityLogic)), skillData.EntityName, new PlayerData(id++, 0)
            {
                //targetType = skillTimeline.TargetType,               
                Rotation = skillData.localRotation,
                LocalScale = skillData.localScale,
                Position = skillData.localeftPostion,              
            });
        }

        public override void Update(float currentTime, float previousTime)
        {
            base.Update(currentTime, previousTime);
            Log.Info("{0}upodateCDClip name：{1}", LogConst.SKillTimeline, GetType().Name);
        }

        public override void Exit()
        {
            base.Exit();
            Log.Info("{0}离开CD name：{1}", LogConst.SKillTimeline, GetType().Name);
        }


    }
}


