using Sirenix.Serialization;
using Slate;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    /// <summary>
    /// 释后技能数据模板基类
    /// </summary>
    [Serializable]
    public class SkillDataBase
    {
        [SerializeField]
        private SkillDataType m_DataType;
        [SerializeField]
        private string m_ResouceName = "None";
        [SerializeField]
        private float m_StartTime;
        [SerializeField]
        private float m_EndTime;

        public SkillDataType DataType
        {
            get { return m_DataType; }
            set { m_DataType = value; }
        }

        public string ResouceName
        {
            get { return m_ResouceName; }
            set { m_ResouceName = value; }
        }

        public float StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = value; }
        }

        public float EndTime
        {
            get { return m_EndTime; }
            set { m_EndTime = value; }
        }

        public SkillDataBase() { }

        public virtual void OnInit(ActionClip data)
        {
            m_StartTime = data.startTime;

            m_EndTime = data.endTime;           
        }
    }

    [Serializable]
    public class SkillData 
    {
        /// <summary>
        /// 技能名
        /// </summary>
        public string SkillName;

        public float Length;
        public float FinishStateTime;
        public float CDTime;
        public Entity Entity;

        public List<SkillDataBase> skillData = new List<SkillDataBase>();

        public TargetType targetType;

        public SkillData() { }

        public SkillData(List<SkillDataBase> skillDatas)
        {
            skillData = skillDatas;
        }
    }


    /// <summary>
    /// 技能数据的类型
    /// 数据的源头来自于timeline编辑的导出数据
    /// </summary>
    [Serializable]
    public enum SkillDataType
    {
        None,
        Animation,
        Effect,
        Collider,
        Move,
        Camera,
        Sound,
        CD,
        Buff,
        InOut,
        FinishState
    }

}

