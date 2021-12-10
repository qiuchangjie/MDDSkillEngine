using System;

namespace MDDSkillEngine
{

    [Flags]
    public enum Team
    {
        /// <summary>
        /// 双方队伍
        /// </summary>
        TEAM_BOTH = 1 << 0,

        /// <summary>
        /// 普通错误
        /// </summary>
        TEAM_CUSTOM = 1 << 1,

        /// <summary>
        /// 敌人
        /// </summary>
        TEAM_ENEMY = 1 << 2,

        /// <summary>
        /// 友军
        /// </summary>
        TEAM_FRIENDLY = 1 << 3,

        /// <summary>
        /// 无
        /// </summary>
        TEAM_NONE = 1 << 4
    }

    [Flags]
    public enum EType
    {

        ALL = 1 << 0,

        BASIC = 1 << 1,

        /// <summary>
        /// 建筑
        /// </summary>
        BUILDING = 1 << 2,

        /// <summary>
        /// 信使
        /// </summary>
        COURIER = 1 << 3,

        /// <summary>
        /// 野怪
        /// </summary>
        CREEP = 1 << 4,

        /// <summary>
        /// 普通
        /// </summary>
        CUSTOM = 1 << 5,

        /// <summary>
        /// 英雄
        /// </summary>
        HERO = 1 << 6,

        /// <summary>
        /// 机械
        /// </summary>
        MECHANICAL = 1 << 7,

        /// <summary>
        /// 无
        /// </summary>
        NONE = 1 << 8,

        /// <summary>
        /// 其他
        /// </summary>
        OTHER = 1 << 9,

        /// <summary>
        /// 树
        /// </summary>
        TREE = 1 << 10,

        /// <summary>
        /// 幻象
        /// </summary>
        ILLUSIONS = 1 << 11,

        /// <summary>
        /// 远古
        /// </summary>
        ANCIENTS = 1 << 12
    }


    [Flags]
    public enum BuffState
    {
        /// <summary>
        /// 死亡
        /// </summary>
        DEAD = 0 << 1,

        /// <summary>
        /// 无敌
        /// </summary>
        WHOSYOURDADDY = 1 << 1,

        /// <summary>
        /// 眩晕
        /// </summary>
        STUNNED = 1 << 2,

        /// <summary>
        /// 禁用伤害减免
        /// </summary>
        BLOCK_DISABLED,


        ATTACK_IMMUNE,

        BLIND,

        FROZEN,

        MISS,

        BAR,

        COLLISION,
    }

    public struct AttackNum
    {
        public int min;

        public int max;

        public AttackNum(int min,int max)
        {
            this.min = min;
            this.max = max;
        }

        public static AttackNum operator + (int addAttackNum , AttackNum attackNum)
        {
            return new AttackNum(addAttackNum + attackNum.min, addAttackNum + attackNum.max);
        }

        public static AttackNum operator - (int addAttackNum, AttackNum attackNum)
        {
            return new AttackNum(attackNum.min - addAttackNum, attackNum.max - addAttackNum);
        }
    }
}
