using MDDGameFramework;
using MDDGameFramework.Runtime;
using System;
using UnityEngine;


namespace MDDSkillEngine
{
    public class Dubao : BuffBase
    {
        DubaoData data;

        public Predicate<DRBuff> drCondition;

        private bool dRCondition(DRBuff buff)
        {
            if (buff.Name == this.GetType().Name && buff.Level == 1)
            {
                return true;
            }
            return false;
        }

        public override void OnInit(IBuffSystem buffSystem, object target, object from, BuffDatabase buffDatabase = null)
        {
            drCondition = dRCondition;

            IDataTable<DRBuff> dtBuff = Game.DataTable.GetDataTable<DRBuff>();

            DRBuff drBuff = dtBuff.GetDataRow(drCondition);

            data = new DubaoData();

            data.Init(drBuff);

            base.OnInit(buffSystem, target, from, data);
           
        }

        public override void OnExecute(IBuffSystem buffSystem)
        {
            Log.Error("dubao!!!!");
        }

        public override void OnUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(buffSystem, elapseSeconds, realElapseSeconds);            
        }


        public override void Clear()
        {
            buffData = null;
        }

        public override void OnFininsh(IBuffSystem buffSystem)
        {
            base.OnFininsh(buffSystem);
            TargetableObject entity = buffSystem.Owner as TargetableObject;

            Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70004)
            {
                Position = entity.CachedTransform.position,
                Rotation = entity.CachedTransform.rotation              
            });

            entity.ApplyDamage((Entity)From, data.Damage);
        }

        
    }
}


