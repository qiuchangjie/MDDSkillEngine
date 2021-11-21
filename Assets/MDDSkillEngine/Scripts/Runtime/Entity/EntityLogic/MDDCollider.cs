using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class MDDCollider : Entity
    {
        ColliderData data;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            data = userData as ColliderData;
            
        }


        private void OnTriggerEnter(Collider other)
        {
            Log.Error("触发碰撞");

            Entity entity = other.gameObject.GetComponent<Entity>();

            if (entity == null)
            {
                return;
            }
            TargetableObject target = entity as TargetableObject;

          

            Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70003)
            {
                Position = entity.CachedTransform.position,
                Rotation = entity.CachedTransform.rotation
            });

            int entityDamageHP = AIUtility.CalcDamageHP(200, 0);

            target.ApplyDamage(target, entityDamageHP);

            if (target.IsDead)
            {
                Game.Fsm.GetFsm<Enemy>(target.Id.ToString()).SetData<VarBoolean>("died", true);
                Game.Entity.HideEntity(this);
                return;
            }

            Game.Buff.AddBuff(target.Id.ToString(), "Dubao", this, data.Owner);

            Game.Fsm.GetFsm<Enemy>(target.Id.ToString()).SetData<VarBoolean>("damage", true);

            // AIUtility.PerformCollision((TargetableObject)data.Owner, entity);

            Game.Entity.HideEntity(this);
        }
    }

}

