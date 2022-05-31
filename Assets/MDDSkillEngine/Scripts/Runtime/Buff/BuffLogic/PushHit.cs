using MDDGameFramework;
using MDDGameFramework.Runtime;
using System;
using UnityEngine;

namespace MDDSkillEngine
{
    public class PushHit : BuffBase
    {
        NormalHitData data;

        public Predicate<DRBuff> drCondition;

        HitData hitData;

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

            DRBuff drBuff = dtBuff.GetDataRow(drCondition);

            data = NormalHitData.Create(drBuff);

            HitData hit = userData as HitData;
            if (hit != null)
            {
                hitData = hit;
            }
            else
            {
                Log.Error("{0}碰撞信息为空", LogConst.Buff);
            }

            base.OnInit(buffSystem, target, from, data);

        }

        public override void OnExecute(IBuffSystem buffSystem)
        {
            Entity entity = Target as Entity;

            if (entity == null)
            {
                Log.Error("{0}hitbuff 目标丢失", LogConst.Buff);
            }

            IFsm<Entity> fsm = Game.Fsm.GetFsm<Entity>(entity.Id.ToString());

            fsm.Blackboard.Set<VarBoolean>(typeof(AiDamageState).Name, true);

            Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70003)
            {
                Position = hitData.HitPoint,
                Rotation = entity.CachedTransform.rotation
            });

            Log.Info("{0}hitbuff", LogConst.Buff);
        }

        public override void OnUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(buffSystem, elapseSeconds, realElapseSeconds);
        }


        public override void Clear()
        {
            base.Clear();
            ReferencePool.Release(data);

            data = null;
        }

        public override void OnFininsh(IBuffSystem buffSystem)
        {
            base.OnFininsh(buffSystem);

            if (hitData != null)
            {
                ReferencePool.Release(hitData);
            }

            Log.Info("{0}hitbuff finish", LogConst.Buff);
        }


    }
}


