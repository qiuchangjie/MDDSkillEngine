using System;
using UnityEngine;

namespace MDDSkillEngine
{
    [Serializable]
    public abstract class EntityData
    {
        [SerializeField]
        private int m_Id = 0;

        [SerializeField]
        private int m_TypeId = 0;

        [SerializeField]
        private Vector3 m_Position = Vector3.zero;

        [SerializeField]
        private Quaternion m_Rotation = Quaternion.identity;

        [SerializeField]
        private Vector3 m_LocalScale = Vector3.one;

        [SerializeField]
        private EntityType m_EntityType = EntityType.Normal;

        /// <summary>
        /// 是否预加载
        /// </summary>
        private bool isPreLoad;

        private bool canSelect=true;

        public EntityData(int entityId, int typeId)
        {
            m_Id = entityId;
            m_TypeId = typeId;
        }

        /// <summary>
        /// 实体编号。
        /// </summary>
        public int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 实体类型编号。
        /// </summary>
        public int TypeId
        {
            get
            {
                return m_TypeId;
            }
        }

        /// <summary>
        /// 实体位置。
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        /// <summary>
        /// 实体朝向。
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                return m_Rotation;
            }
            set
            {
                m_Rotation = value;
            }
        }


        public Vector3 LocalScale
        {
            get
            {
                return m_LocalScale;
            }
            set
            {
                m_LocalScale = value;
            }
        }

        public bool IsPreLoad
        {
            get
            {
                return isPreLoad;
            }
            set
            {
                isPreLoad = value;
            }
        }

        public bool IsCanSelect
        {
            get
            {
                return canSelect;
            }
        }

        public EntityType EntityType
        {
            get 
            {
                return m_EntityType;
            }
            
            set { m_EntityType = value; }
        }
    }
}