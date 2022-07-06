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

            //碰撞体层级设置
            if (data.Owner != null)
            {
                if (data.Owner.gameObject.layer == LayerMask.NameToLayer("player"))
                {
                    gameObject.layer = LayerMask.NameToLayer("Collider");
                }
                else if (data.Owner.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    gameObject.layer = LayerMask.NameToLayer("ColliderEnemy");
                }
            }

            Collider Col = GetComponent<Collider>();

            if (Col is BoxCollider)
            {
                BoxCollider Box = (BoxCollider)Col;
                Box.size = data.boundSize;
                Box.center = data.boundCenter;
            }
            else if (Col is SphereCollider)
            {
                SphereCollider Sphere = (SphereCollider)Col;
                Sphere.center = data.boundCenter;
                Sphere.radius = data.radius;
            }
            else if (Col is CapsuleCollider)
            {
                CapsuleCollider Capsule = (CapsuleCollider)Col;
                Capsule.height = data.height;
                Capsule.radius = data.radius;
                Capsule.center = data.boundCenter;
            }
         
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

            Vector3 hitPos = other.ClosestPoint(transform.position);

            Vector3 hitDir = (new Vector3(entity.transform.position.x, 0, entity.transform.position.z) - new Vector3(transform.position.x, 0f, transform.position.z)).normalized;

            if (data.buffName == "")
                Game.Buff.AddBuff(entity.Id.ToString(), "NormalHit", entity, data.Owner, HitData.Create(this, entity, hitPos, hitDir, EffectName: data.HitEffectName));
            else
                Game.Buff.AddBuff(entity.Id.ToString(), data.buffName, entity, data.Owner, HitData.Create(this, entity, hitPos, hitDir, EffectName: data.HitEffectName));
        }
    }

}

