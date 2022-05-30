using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class NormalMoveCollider : ColliderBase
    {
        MoveColliderData data;

        private int damageSettlementPreSecond = 4;

        private bool canDamage;

        private float waitTime;

        private float needWaitTime;

        private int effectid;

        Vector3 dir;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);


        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            data = userData as MoveColliderData;

            needWaitTime = 1f / damageSettlementPreSecond;

            effectid = Game.Entity.GenerateSerialId();

            Game.Entity.ShowEffect(new EffectData(effectid, 70007)
            {
                Owner = this,
                Position = CachedTransform.position,
                Rotation = CachedTransform.rotation,
                LocalScale = new Vector3(0.2f,0.2f,0.2f),
                KeepTime = 999
                
            });
                

            dir = data.Owner.CachedTransform.forward;
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            data.Duration -= elapseSeconds;
            if (data.Duration <= 0)
            {
                Log.Info("{0}回收collider, name :{1}", LogConst.Skill, Name);
                HideSelf();
            }

            MoveWithDirAndSpeed(data.Dir, data.Speed, elapseSeconds);

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

