//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2022-05-13 23:27:01.121
//------------------------------------------------------------

using MDDGameFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    /// <summary>
    /// 英雄配置表。
    /// </summary>
    public class DRHero : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取Heroid。
        /// </summary>
        public override int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 获取名字。
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取外号。
        /// </summary>
        public string NickName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取资源名。
        /// </summary>
        public string AssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取图标。
        /// </summary>
        public string Icon
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取是否是近战。
        /// </summary>
        public bool Melee
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取阵营。
        /// </summary>
        public int Team
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取英雄类型1.力量    2.智力   3.敏捷。
        /// </summary>
        public int HeroType
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取关联的技能。
        /// </summary>
        public List<int> Skill
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基础血量。
        /// </summary>
        public int HP
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基础攻击力。
        /// </summary>
        public List<int> Attack
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取攻击距离。
        /// </summary>
        public int AttackDistance
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取移动速度。
        /// </summary>
        public int MoveSpeed
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基础攻击间隔。
        /// </summary>
        public float BasicAttackInterval
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取力量。
        /// </summary>
        public float Strength
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取智力。
        /// </summary>
        public float Intelligence
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取敏捷。
        /// </summary>
        public float Agile
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取力量成长系数。
        /// </summary>
        public float StrengthGF
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取智力成长系数。
        /// </summary>
        public float IntelligenceGF
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取敏捷成长系数。
        /// </summary>
        public float AgileGF
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取基础护甲。
        /// </summary>
        public float Armor
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取生命恢复。
        /// </summary>
        public float HPRecovery
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取复活时间。
        /// </summary>
        public float ReliveTime
        {
            get;
            private set;
        }

        public override bool ParseDataRow(string dataRowString, object userData)
        {
            string[] columnStrings = dataRowString.Split(DataTableExtension.DataSplitSeparators);
            for (int i = 0; i < columnStrings.Length; i++)
            {
                columnStrings[i] = columnStrings[i].Trim(DataTableExtension.DataTrimSeparators);
            }

            int index = 0;
            index++;
            m_Id = int.Parse(columnStrings[index++]);
            index++;
            Name = columnStrings[index++];
            NickName = columnStrings[index++];
            AssetName = columnStrings[index++];
            Icon = columnStrings[index++];
            Melee = bool.Parse(columnStrings[index++]);
            Team = int.Parse(columnStrings[index++]);
            HeroType = int.Parse(columnStrings[index++]);
            Skill =  DataTableExtension.ParseList(columnStrings[index++]);
            HP = int.Parse(columnStrings[index++]);
            Attack =  DataTableExtension.ParseList(columnStrings[index++]);
            AttackDistance = int.Parse(columnStrings[index++]);
            MoveSpeed = int.Parse(columnStrings[index++]);
            BasicAttackInterval = float.Parse(columnStrings[index++]);
            Strength = float.Parse(columnStrings[index++]);
            Intelligence = float.Parse(columnStrings[index++]);
            Agile = float.Parse(columnStrings[index++]);
            StrengthGF = float.Parse(columnStrings[index++]);
            IntelligenceGF = float.Parse(columnStrings[index++]);
            AgileGF = float.Parse(columnStrings[index++]);
            Armor = float.Parse(columnStrings[index++]);
            HPRecovery = float.Parse(columnStrings[index++]);
            ReliveTime = float.Parse(columnStrings[index++]);

            GeneratePropertyArray();
            return true;
        }

        public override bool ParseDataRow(byte[] dataRowBytes, int startIndex, int length, object userData)
        {
            using (MemoryStream memoryStream = new MemoryStream(dataRowBytes, startIndex, length, false))
            {
                using (BinaryReader binaryReader = new BinaryReader(memoryStream, Encoding.UTF8))
                {
                    m_Id = binaryReader.Read7BitEncodedInt32();
                    Name = binaryReader.ReadString();
                    NickName = binaryReader.ReadString();
                    AssetName = binaryReader.ReadString();
                    Icon = binaryReader.ReadString();
                    Melee = binaryReader.ReadBoolean();
                    Team = binaryReader.Read7BitEncodedInt32();
                    HeroType = binaryReader.Read7BitEncodedInt32();
                    Skill = binaryReader.ReadList();
                    HP = binaryReader.Read7BitEncodedInt32();
                    Attack = binaryReader.ReadList();
                    AttackDistance = binaryReader.Read7BitEncodedInt32();
                    MoveSpeed = binaryReader.Read7BitEncodedInt32();
                    BasicAttackInterval = binaryReader.ReadSingle();
                    Strength = binaryReader.ReadSingle();
                    Intelligence = binaryReader.ReadSingle();
                    Agile = binaryReader.ReadSingle();
                    StrengthGF = binaryReader.ReadSingle();
                    IntelligenceGF = binaryReader.ReadSingle();
                    AgileGF = binaryReader.ReadSingle();
                    Armor = binaryReader.ReadSingle();
                    HPRecovery = binaryReader.ReadSingle();
                    ReliveTime = binaryReader.ReadSingle();
                }
            }

            GeneratePropertyArray();
            return true;
        }

        private void GeneratePropertyArray()
        {

        }
    }
}
