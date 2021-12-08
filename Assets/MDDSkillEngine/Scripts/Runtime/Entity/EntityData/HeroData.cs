using System;
using UnityEngine;

namespace MDDSkillEngine
{
    [Serializable]
    public abstract class HeroData : EntityData
    {
        [SerializeField]
        private Team m_Team = Team.TEAM_BOTH;

        [SerializeField]
        private int m_HP = 0;

        [SerializeField]
        private float m_Speed = 0;

        public HeroData(int entityId, int typeId)
            : base(entityId, typeId)
        {
           
        }

        /// <summary>
        /// 角色阵营。
        /// </summary>
        public Team Team
        {
            get
            {
                return m_Team;
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
