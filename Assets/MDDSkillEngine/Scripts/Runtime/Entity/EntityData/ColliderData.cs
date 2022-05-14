using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class ColliderData : EntityData
    {
        private Entity m_Owner;

        private float m_duration = 999;

        public Entity Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public float Duration
        {
            get
            {
                return m_duration;
            }
            set
            {
                m_duration = value;
            }
        }


        public ColliderData(int entityId, int typeId, Entity owner)
           : base(entityId, typeId)
        {
            m_Owner = owner;
            EntityType = EntityType.Collider;
        }
    }

}

