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
            base.OnShow(userData);

            data = userData as ColliderData;

            needWaitTime = 1f / damageSettlementPreSecond;

            if (data.IsFollowParent && !data.IsPreLoad)
            {
                Game.Entity.AttachEntity(Id, data.Owner.Id);
                BoxCollider Box = GetComponent<BoxCollider>();
                Box.size = data.boundSize;
                Box.center = data.boundCenter;
                CachedTransform.localScale = data.localScale;
                CachedTransform.localPosition = data.localeftPostion;
                CachedTransform.localRotation = data.localRotation;
            }
            else if (!data.IsFollowParent && !data.IsPreLoad)
            {
                Game.Entity.AttachEntity(Id, data.Owner.Id);
                BoxCollider Box = GetComponent<BoxCollider>();
                Box.size = data.boundSize;
                Box.center = data.boundCenter;
                CachedTransform.localScale = data.localScale;
                CachedTransform.localPosition = data.localeftPostion;
                CachedTransform.localRotation = data.localRotation;
                Game.Entity.DetachEntity(Id);
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (data.Duration <= wasDuration)
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

            if (data.buffName == "")
                Game.Buff.AddBuff(entity.Id.ToString(), "NormalHit", entity, data.Owner, HitData.Create(this, entity, hitPos, this.transform.forward, EffectName: data.HitEffectName));
            else
                Game.Buff.AddBuff(entity.Id.ToString(), data.buffName, entity, data.Owner, HitData.Create(this, entity, hitPos, this.transform.forward, EffectName: data.HitEffectName));
        }
    }

}

