using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class MDDCollider : ColliderBase
    {
        private int damageSettlementPreSecond = 4;

        private bool canDamage;

        private float waitTime;

        private float needWaitTime;


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);


        }

        protected override void OnShow(object userData)
        {
            data = userData as ColliderData;
            base.OnShow(userData);

            BoxCollider Box = GetComponent<BoxCollider>();
            Box.size = data.boundSize;
            Box.center = data.boundCenter;

            needWaitTime = 1f / damageSettlementPreSecond;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

          

            waitTime += elapseSeconds;

            if (waitTime >= needWaitTime)
            {
                canDamage = true;
                waitTime -= needWaitTime;
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            Log.Error("触发碰撞");

            Entity entity = other.gameObject.GetComponent<Entity>();

            if (entity == null)
            {
                return;
            }

            Vector3 hitPos = other.ClosestPoint(CachedTransform.position);

            if (data.buffName == "")
                Game.Buff.AddBuff(entity.Id.ToString(), "NormalHit", entity, data.Owner, HitData.Create(this, entity, hitPos, this.transform.forward, EffectName: data.HitEffectName));
            else
                Game.Buff.AddBuff(entity.Id.ToString(), data.buffName, entity, data.Owner, HitData.Create(this, entity, hitPos, this.transform.forward, EffectName: data.HitEffectName));
        }
    }

}

