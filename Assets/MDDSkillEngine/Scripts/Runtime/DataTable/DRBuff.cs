//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2021-12-12 00:36:43.424
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
    /// Buff配置表。
    /// </summary>
    public class DRBuff : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取buffid。
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
        /// 获取buff类型。
        /// </summary>
        public int Type
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取等级。
        /// </summary>
        public int Level
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取持续时间。
        /// </summary>
        public float Duration
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取结束时的一次性伤害。
        /// </summary>
        public int OverDamage
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取持续伤害。
        /// </summary>
        public int DoT
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
            Type = int.Parse(columnStrings[index++]);
            Level = int.Parse(columnStrings[index++]);
            Duration = float.Parse(columnStrings[index++]);
            OverDamage = int.Parse(columnStrings[index++]);
            DoT = int.Parse(columnStrings[index++]);

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
                    Type = binaryReader.Read7BitEncodedInt32();
                    Level = binaryReader.Read7BitEncodedInt32();
                    Duration = binaryReader.ReadSingle();
                    OverDamage = binaryReader.Read7BitEncodedInt32();
                    DoT = binaryReader.Read7BitEncodedInt32();
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
