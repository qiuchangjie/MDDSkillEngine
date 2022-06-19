using System;
using UnityEngine;

namespace MDDSkillEngine
{
    [Serializable]
    public abstract class SummonedBaseData : EntityData
    {
        [SerializeField]
        private int m_HP = 0;

        [SerializeField]
        private float m_Speed = 0;

        private Entity m_Owner = null;

        public SummonedBaseData(int entityId, int typeId, Entity Owner)
            : base(entityId, typeId)
        {
            m_Owner = Owner;
            m_HP = 1000;
            m_Speed = 10;
        }

        /// <summary>
        /// 当前生命。
        /// </summary>
        public int HP
        {
            get
            {
                return m_HP;
            }
            set
            {
                m_HP = value;
            }
        }

        /// <summary>
        /// 拥有者
        /// </summary>
        public Entity Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
        }

        /// <summary>
        /// 最大生命。
        /// </summary>
        public abstract int MaxHP
        {
            get;
        }

        public float Speed
        {
            get
            {
                return m_Speed;
            }
            set
            {
                m_Speed = value;
            }
        }

        /// <summary>
        /// 生命百分比。
        /// </summary>
        public float HPRatio
        {
            get
            {
                return MaxHP > 0 ? (float)HP / MaxHP : 0f;
            }
        }
    }
}
