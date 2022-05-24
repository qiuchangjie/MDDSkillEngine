using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class MDDCollider : Entity
    {
        ColliderData data;

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
            base.OnShow(userData);

            data = userData as ColliderData;

            needWaitTime = 1f / damageSettlementPreSecond;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            data.Duration -= elapseSeconds;
            if (data.Duration <= 0)
            {
                HideSelf();
            }

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

            Game.Event.Fire(this, ColliderEnterEventArgs.Create(data.Owner, entity, hitPos));
        }


        private void OnTriggerStay(Collider other)
        {
            if (!canDamage)
            {
                return;
            }

            Log.Error("触发Stay碰撞");

            Entity entity = other.gameObject.GetComponent<Entity>();

            if (entity == null)
            {
                return;
            }

            Game.Buff.AddBuff(entity.Id.ToString(), "NormalHit", entity, data.Owner);

            Vector3 hitPos = other.ClosestPoint(CachedTransform.position);

            Game.Event.Fire(this, ColliderEnterEventArgs.Create(data.Owner, entity, hitPos));

            canDamage = false;
        }

      
    }

}

