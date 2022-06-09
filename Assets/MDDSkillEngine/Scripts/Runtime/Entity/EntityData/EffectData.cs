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

        public Vector3 localeftPostion;

        public Quaternion localRotation;

        public Vector3 localScale;

        public TargetType targetType;
        public bool hasPath;
        public bool useSpeed;
        public Vector3[] bezierPath;
        public Vector3 bezierPathParentPosition;
        public Quaternion bezierPathParentRotation;
        public float bezierPathLength;

        public EffectData(int entityId, int typeId)
            : base(entityId, typeId)
        {
            //m_KeepTime = 3f;
            EntityType = EntityType.Effect;
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
            get 
            { 
                return isFllow; 
            }
            set 
            { 
                isFllow = value; 
            }
        }
    }
}
