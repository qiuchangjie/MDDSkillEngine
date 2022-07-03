using MDDGameFramework;
using MDDGameFramework.Runtime;
using System;
using UnityEngine;


namespace MDDSkillEngine
{
    public class PlaybleSpeedTestBuff : BuffBase
    {
        NormalData data;

        public Predicate<DRBuff> drCondition;

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

            data = NormalData.Create(drBuff);

            base.OnInit(buffSystem, target, from, data);

        }

        public override void OnExecute(IBuffSystem buffSystem)
        {
            Entity entity = Target as Entity;

            if (entity == null)
            {
                Log.Error("{0}PlaybleSpeedTestBuff", LogConst.Buff);
            }

            Game.Entity.ShowEffect(typeof(Effect), "KaerShengbo", new EffectData(Game.Entity.GenerateSerialId(), 0)
            {
                Owner = entity,
                KeepTime = 90f,
                IsFllow = true,            
                LocalScale = new Vector3(1f, 1f, 1f),
                localScale = new Vector3(1f, 1f,1f)
            });

            entity.SetState(EntityNormalState.SPACEWALK , true);

            //Log.Error("{0}PlaybleSpeedTestBuff", LogConst.Buff);
        }

        public override void OnUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(buffSystem, elapseSeconds, realElapseSeconds);
            if (buffSystem.Owner is Entity)
            {
                ((Entity)buffSystem.Owner).CachedTransform.position += new Vector3(-2f, 0, 0f) * elapseSeconds * buffSystem.PlayableSpeed;
            }
        }


        public override void Clear()
        {
            base.Clear();
        }

        public override void OnFininsh(IBuffSystem buffSystem)
        {
            base.OnFininsh(buffSystem);
            Log.Error("{0}PlaybleSpeedTestBuff", LogConst.Buff);
        }


    }
}


