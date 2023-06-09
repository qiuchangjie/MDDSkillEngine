﻿using System;
using UnityEngine;

namespace MDDSkillEngine
{
    [Serializable]
    public abstract class TargetableObjectData : EntityData
    {
        [SerializeField]
        private CampType m_Camp = CampType.Unknown;

        [SerializeField]
        private int m_HP = 0;

        [SerializeField]
        private float m_Speed = 0;

        public Vector3 localeftPostion;

        public Quaternion localRotation;

        public Vector3 localScale;

        public TargetableObjectData(int entityId, int typeId, CampType camp)
            : base(entityId, typeId)
        {
            m_Camp = camp;
            m_HP = 0;
            EntityType = EntityType.Hero;
        }

        /// <summary>
        /// 角色阵营。
        /// </summary>
        public CampType Camp
        {
            get
            {
                return m_Camp;
            }
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
