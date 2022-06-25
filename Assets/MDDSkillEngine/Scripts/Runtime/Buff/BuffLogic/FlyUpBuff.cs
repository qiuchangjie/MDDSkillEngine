using MDDGameFramework;
using MDDGameFramework.Runtime;
using System;
using UnityEngine;

namespace MDDSkillEngine
{
    public class FlyUpBuff : BuffBase
    {
        NormalData data;

        public Predicate<DRBuff> drCondition;

        DRBuff drBuff;

        HitData hitData;

        private float speed = 3.5f;
        private float height = 2f;
        private bool isUp;
        private float downTime;

        private Vector3 cachePoint;

        private bool dRCondition(DRBuff buff)
        {
            if (buff.Name == this.GetType().Name && buff.Level == 1)
            {
                return true;
            }
            return false;
        }

        public override void OnInit(IBuffSystem buffSystem, object target, object from, BuffDatabase buffDatabase = null, object userData = null)
        {
            drCondition = dRCondition;

            IDataTable<DRBuff> dtBuff = Game.DataTable.GetDataTable<DRBuff>();

            drBuff = dtBuff.GetDataRow(drCondition);

            HitData hit = userData as HitData;
            if (hit != null)
            {
                hitData = hit;
            }
            else
            {
                Log.Error("{0}碰撞信息为空", LogConst.Buff);
            }

            data = NormalData.Create(drBuff);   

            base.OnInit(buffSystem, target, from, data);

        }

        public override void OnExecute(IBuffSystem buffSystem)
        {
            Entity entity = Target as Entity;

            if (entity == null)
            {
                Log.Error("{0}hitbuff 目标丢失", LogConst.Buff);
            }

            downTime = height / speed;
            cachePoint = entity.CachedTransform.position;


            entity.SetState(EntityNormalState.FLYSKY, true);

            Game.Entity.ShowEffect(typeof(Effect), "fengbaodown", new EffectData(Game.Entity.GenerateSerialId(), 0)
            {
                KeepTime = drBuff.Duration,
                Position = cachePoint,
                Rotation = entity.CachedTransform.rotation,
                LocalScale = new Vector3(0.1f,0.2f,0.1f)
            }) ;

            entity.CachedAnimancer.Playable.Speed = 0.5f;

            Log.Info("{0}FlyUpBuff", LogConst.Buff);
        }

        public override void OnUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(buffSystem, elapseSeconds, realElapseSeconds);
            Entity entity = Target as Entity;

            if (entity.CachedTransform.position.y <= height && !isUp)
            {
                entity.CachedTransform.position += new Vector3(0f, speed * elapseSeconds, 0f);
            }
            else
            {
                isUp = true;
            }

            Log.Error("{0}FlyUpBuff", LogConst.Buff);

            entity.CachedTransform.Rotate(new Vector3(0f,-500f,0f) * elapseSeconds, Space.Self);


            if ((data.Duration - data.PassDuration) <= downTime)
            {
                entity.CachedTransform.position -= new Vector3(0f, speed * elapseSeconds, 0f);
            }

        }

        public override void OnFixedUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            base.OnFixedUpdate(buffSystem, elapseSeconds, realElapseSeconds);
        }


        public override void Clear()
        {
            base.Clear();
        }

        public override void OnFininsh(IBuffSystem buffSystem)
        {
            base.OnFininsh(buffSystem);
            Entity entity = Target as Entity;
            if (entity != null)
            {
                entity.CachedTransform.position = cachePoint;
                entity.SetState(EntityNormalState.FLYSKY,false);
            }

            isUp = false;
            Log.Info("{0}FlyUpBuff finish", LogConst.Buff);
        }


    }
}


