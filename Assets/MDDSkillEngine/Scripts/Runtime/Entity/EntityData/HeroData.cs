using System;
using UnityEngine;

namespace MDDSkillEngine
{
    [Serializable]
    public abstract class HeroData : EntityData
    {
        /// <summary>
        /// 阵营
        /// </summary>
        private Team m_Team = Team.TEAM_BOTH;

        /// <summary>
        /// 血量
        /// </summary>
        private float m_HP = 0;

        /// <summary>
        /// 力量
        /// </summary>
        private int m_Strength = 0;

        /// <summary>
        /// 智力
        /// </summary>
        private int m_Intelligence = 0;

        /// <summary>
        /// 敏捷
        /// </summary>
        private int m_Agile = 0;

        /// <summary>
        /// 攻击速度
        /// 攻击速度 =（100+敏捷 + 固定值加成的累加）*1.7/自身初始攻击间隔
        /// </summary>
        private int m_AttackSpeed = 100;

        /// <summary>
        /// 基础攻击间隔
        /// </summary>
        private float m_BasicAttackInterval = 1.7f;

        /// <summary>
        /// 攻击前摇
        /// 攻击间隔的百分比
        /// </summary>
        private int m_ShakeBeforeattack;

        /// <summary>
        /// 护甲
        /// </summary>
        private int m_Armor;

        /// <summary>
        /// 魔法抗性
        /// </summary>
        private float m_MagicResistance;

        /// <summary>
        /// 攻击距离
        /// </summary>
        private int m_AttackDistance;

        /// <summary>
        /// 移动速度
        /// </summary>
        private int m_MoveSpeed;

        /// <summary>
        /// 攻击间隔
        /// </summary>
        private float m_AttackInterval = 0f;

        /// <summary>
        /// 攻击力
        /// </summary>
        private AttackNum m_Attack;


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
        public float HP
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
        public abstract float MaxHP
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

        /// <summary>
        /// 收到的物理伤害比例 百分比
        /// </summary>
        public double Damagemultiplier
        {
            get
            {
                double temp = 1 - 0.06 * m_Armor / (1 + 0.06 * Math.Abs(m_Armor));

                return Math.Round(temp, 2);
            }
        }

        /// <summary>
        /// 魔法抗性
        /// </summary>
        public float MagicResistance
        {
            get
            {
                return m_MagicResistance;
            }
        }
    }
}
