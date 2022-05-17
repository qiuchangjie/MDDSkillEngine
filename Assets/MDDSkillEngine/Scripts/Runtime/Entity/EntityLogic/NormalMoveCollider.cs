using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class NormalMoveCollider : Entity
    {
        ColliderData data;

        private int damageSettlementPreSecond = 4;

        private bool canDamage;

        private float waitTime;

        private float needWaitTime;

        private int effectid;


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);


        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            data = userData as ColliderData;

            needWaitTime = 1f / damageSettlementPreSecond;

            effectid = Game.Entity.GenerateSerialId();

            Game.Entity.ShowEffect(new EffectData(effectid, 70007)
            {
                Position = CachedTransform.position,
                Rotation = CachedTransform.rotation,
                LocalScale = CachedTransform.localScale,
                KeepTime = 999
            });
            Game.Entity.AttachEntity(effectid, Id);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            data.Duration -= elapseSeconds;
            if (data.Duration <= 0)
            {
                HideSelf();
            }

            MoveWithDirAndSpeed(data.Owner.CachedTransform.forward, data.Speed, elapseSeconds);

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
            Game.Entity.HideEntity(effectid);
        }

        private void MoveWithDirAndSpeed(Vector3 dir,float speed,float elapseSeconds)
        {
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

            Vector3 hitPos = other.ClosestPoint(CachedTransform.position);

            Game.Event.Fire(this, ColliderEnterEventArgs.Create(data.Owner, entity, hitPos));

            canDamage = false;
        }


    }

}

