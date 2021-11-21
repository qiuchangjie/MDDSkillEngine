using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class ColliderData : EntityData
    {
        Entity m_Owner;

        public Entity Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public ColliderData(int entityId, int typeId,Entity owner)
           : base(entityId, typeId)
        {
            m_Owner = owner;
        }
    }

}

