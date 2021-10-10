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
        private float m_Level;
        private float m_Duration;      
        private float m_PassDuration;
   
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
        public float Level
        {
            get { return m_Level; }
        }

        /// <summary>
        /// 持续时间
        /// </summary>
        public float Duration
        {
            get { return m_Duration; }
        }
        
        /// <summary>
        /// buff已经持续的时间
        /// </summary>
        public float PassDuration
        {
            get { return m_PassDuration; }
            set { m_PassDuration = value; }
        }

        /// <summary>
        /// buff行进百分比
        /// 若是永久buff则直接返回百分之百
        /// </summary>
        public float DurationRadio
        {
            get
            {
                return m_Duration > 0 ? m_PassDuration / m_Duration : 1f;
            }
        }
    }
}

