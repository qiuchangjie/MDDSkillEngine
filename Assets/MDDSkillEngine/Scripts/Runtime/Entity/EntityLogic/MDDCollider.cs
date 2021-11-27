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

            Vector3  hitPos = other.ClosestPoint(CachedTransform.position);

            Game.Event.Fire(this, ColliderEnterEventArgs.Create(data.Owner, entity, hitPos));

        }
    }

}

