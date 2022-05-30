using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework
{
    /// <summary>
    /// buff通用数据
    /// </summary>
    public abstract class BuffDatabase :IReference
    {
        private int m_Id;
        private int m_Level;
        private string m_Name;
        private bool m_CanOverlying;
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

        public int UId
        {
            get { return GetHashCode(); }
        }

        /// <summary>
        /// buffname
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
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


        /// <summary>
        /// 积累时间
        /// </summary>
        public float AccumulateDuration
        {
            get { return m_accumulateDuration; }
            set { m_accumulateDuration = value; }
        }

        /// <summary>
        /// 是否可叠加
        /// </summary>
        public bool CanOverlying
        {
            get { return m_CanOverlying; }
            set { m_CanOverlying = value;}
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

        public virtual void Clear()
        {
            m_Id = 0;
            m_Level = 0;
            m_accumulateDuration = 0;
            m_Duration = 0;
            m_PassDuration = 0f;
        }

        public void Init(int id,string name,int level,float duration)
        {
            m_Id = id;
            m_Name = name;
            m_Level = level;
            m_Duration = duration;
        }
    }
}

