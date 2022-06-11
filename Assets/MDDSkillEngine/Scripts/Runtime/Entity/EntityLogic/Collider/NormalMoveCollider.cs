using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class NormalMoveCollider : ColliderBase
    {
        MoveColliderData dataMoveCollider;

        private int damageSettlementPreSecond = 4;

        private bool canDamage;

        private float waitTime;

        private float needWaitTime;


        Vector3 dir;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnShow(object userData)
        {
            data = userData as MoveColliderData;
            dataMoveCollider = userData as MoveColliderData;
            base.OnShow(userData);

            needWaitTime = 1f / damageSettlementPreSecond;

            BoxCollider Box = GetComponent<BoxCollider>();
            Box.size = data.boundSize;
            Box.center = data.boundCenter;

            dir = data.Owner.CachedTransform.forward;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);


            MoveWithDirAndSpeed(dataMoveCollider.Dir, dataMoveCollider.Speed, elapseSeconds);

            waitTime += elapseSeconds;

            if (waitTime >= needWaitTime)
            {
                canDamage = true;
                waitTime -= needWaitTime;
            }
        }

        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }

        private void MoveWithDirAndSpeed(Vector3 dir, float speed, float elapseSeconds)
        {
            if (speed != 0)
                transform.Translate(dir * speed * elapseSeconds);
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
                Game.Buff.AddBuff(entity.Id.ToString(), "NormalHit", entity, data.Owner, HitData.Create(this, entity, hitPos, this.transform.forward));
            else
                Game.Buff.AddBuff(entity.Id.ToString(), data.buffName, entity, data.Owner, HitData.Create(this, entity, hitPos,
                    this.transform.forward, data.HitEffectName));
        }


    }

}

