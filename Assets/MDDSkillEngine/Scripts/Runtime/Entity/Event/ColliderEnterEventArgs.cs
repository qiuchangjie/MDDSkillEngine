using MDDGameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDSkillEngine
{
    public class ColliderEnterEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ColliderEnterEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }


        public Entity Owner
        {
            get;
            private set;
        }

        public Entity Other
        {
            get;
            private set;
        }

        public Vector3 HitPosition
        {
            get;
            private set;
        }

        public static ColliderEnterEventArgs Create(Entity owner, Entity other, Vector3 hitPosition)
        {
            ColliderEnterEventArgs colliderEnterEventArgs = ReferencePool.Acquire<ColliderEnterEventArgs>();
            colliderEnterEventArgs.Owner = owner;
            colliderEnterEventArgs.Other = other;
            colliderEnterEventArgs.HitPosition = hitPosition;

            return colliderEnterEventArgs;
        }



        public override void Clear()
        {
            Owner = null;
            Other = null;
            HitPosition = Vector3.zero;
        }
    }

}

