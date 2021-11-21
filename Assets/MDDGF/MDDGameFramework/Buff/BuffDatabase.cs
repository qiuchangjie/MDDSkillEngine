using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework
{
    /// <summary>
    /// buff通用数据
    /// </summary>
    public abstract class BuffDatabase 
    {
        private int m_Id;
        private int m_Level;
        private float m_Duration;      
        private float m_PassDuration;
        private float m_accumulateDuration;

        /// <summary>
        /// buffID
        /// </summary>
        public int Id
        {
            get { return m_Id; }
        }

        /// <summary>
        /// buff等级
        /// </summary>
        public int Level
        {
            get { return m_Level; }
        }

        /// <summary>
        /// 持续时间
        /// 永久buff持续时间默认为-1
        /// </summary>
        public float Duration
        {
            get { return m_Duration; }
        }
        
        /// <summary>
        /// buff在单次持续时间内已经持续的时间
        /// </summary>
        public float PassDuration
        {
            get { return m_PassDuration; }
            set { m_PassDuration = value; }
        }

        public float AccumulateDuration
        {
            get { return m_accumulateDuration; }
            set { m_accumulateDuration = value; }
        }

        /// <summary>
        /// buff行进百分比
        /// 若是永久buff则直接返回百分之百
        /// 永久buff持续时间默认为-1
        /// </summary>
        public float DurationRadio
        {
            get
            {
                return m_Duration > 0 ? m_PassDuration / m_Duration : 1f;
            }
        }

        public void Init(int id,int level,float duration)
        {
            m_Id = id;
            m_Level = level;
            m_Duration = duration;
        }
    }
}

