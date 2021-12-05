using System;
using UnityEngine;

namespace MDDSkillEngine
{
    [Serializable]
    public class EffectData : EntityData
    {
        [SerializeField]
        private float m_KeepTime = 3f;

        private Entity m_Owner;

        private bool isFllow;


        public EffectData(int entityId, int typeId)
            : base(entityId, typeId)
        {
            //m_KeepTime = 3f;
        }

        public float KeepTime
        {
            get
            {
                return m_KeepTime;
            }
            set
            {
                m_KeepTime = value;
            }
        }

        public Entity Owner
        {
            get
            {
                return m_Owner;
            }
            set
            {
                m_Owner = value;
            }
        }

        public bool IsFllow

        {
            get { return isFllow; }
            set { isFllow = value; }
        }
    }
}
