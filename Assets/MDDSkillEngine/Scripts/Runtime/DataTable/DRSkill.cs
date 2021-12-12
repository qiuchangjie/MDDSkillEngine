﻿//------------------------------------------------------------
// 此文件由工具自动生成，请勿直接修改。
// 生成时间：2021-12-12 00:36:43.427
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
    /// 技能配置表。
    /// </summary>
    public class DRSkill : DataRowBase
    {
        private int m_Id = 0;

        /// <summary>
        /// 获取skillid。
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
        /// 获取资源名。
        /// </summary>
        public string AssetName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取技能图标。
        /// </summary>
        public string Icon
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取关联的特效资源id。
        /// </summary>
        public List<int> EffectAsset
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取会生成多份的特效。
        /// </summary>
        public List<int> EffectAssetMutl
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取会使用到的碰撞体。
        /// </summary>
        public List<int> ColliderEntity
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取需要生成多份的碰撞体。
        /// </summary>
        public List<int> ColliderEntityMutl
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
            AssetName = columnStrings[index++];
            Icon = columnStrings[index++];
            EffectAsset =  DataTableExtension.ParseList(columnStrings[index++]);
            EffectAssetMutl =  DataTableExtension.ParseList(columnStrings[index++]);
            ColliderEntity =  DataTableExtension.ParseList(columnStrings[index++]);
            ColliderEntityMutl =  DataTableExtension.ParseList(columnStrings[index++]);

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
                    AssetName = binaryReader.ReadString();
                    Icon = binaryReader.ReadString();
                    EffectAsset = binaryReader.ReadList();
                    EffectAssetMutl = binaryReader.ReadList();
                    ColliderEntity = binaryReader.ReadList();
                    ColliderEntityMutl = binaryReader.ReadList();
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
