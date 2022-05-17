﻿using System.Collections.Generic;
using Sirenix.OdinInspector;
using Vector3 = System.Numerics.Vector3;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

#endif

namespace MDDGameFramework
{
    /// <summary>
    /// 与黑板节点相关的数据
    /// </summary>
    [BoxGroup("黑板数据配置")]
    [HideLabel]
    [ShowOdinSerializedPropertiesInInspector]
    public class ClassForBlackboard
    {
        [LabelText("字典键")]
        [ValueDropdown("GetBBKeys")]
        [OnValueChanged("OnBBKeySelected")]
        public string BBKey;

        [LabelText("指定的值类型")]
        [ReadOnly]
        public string NP_BBValueType;

        [LabelText("写入或比较")]
        public bool WriteOrCompareToBB;

        [ShowIf("WriteOrCompareToBB")]
        public Variable NP_BBValue;


#if UNITY_EDITOR
        private IEnumerable<string> GetBBKeys()
        {
            if (NPBlackBoardEditorInstance.AllBB != null)
            {
                return NPBlackBoardEditorInstance.AllBB.Keys;
            }

            return null;
        }

        private void OnBBKeySelected()
        {
            if (NPBlackBoardEditorInstance.AllBB != null)
            {
                foreach (var bbValues in NPBlackBoardEditorInstance.AllBB)
                {
                    if (bbValues.Key == this.BBKey)
                    {
                        NP_BBValue = bbValues.Value.DeepCopy();
                        NP_BBValueType = this.NP_BBValue.Type.ToString();
                    }
                }
            }
        }
#endif

        /// <summary>
        /// 获取目标黑板对应的此处的键的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetBlackBoardValue<T>(Blackboard blackboard) where T : Variable
        {
            return blackboard.Get<T>(this.BBKey);
        }

        /// <summary>
        /// 获取配置的BB值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetTheBBDataValue<T>()
        {
            Variable<T> var = NP_BBValue as Variable<T>;

            return var.Value;
        }

        /// <summary>
        /// 自动根据预先设定的值设置值
        /// </summary>
        /// <param name="blackboard">要修改的黑板</param>
        public void SetBlackBoardValue(Blackboard blackboard)
        {
            blackboard.Set(BBKey, NP_BBValue);

            
        }

        /// <summary>
        /// 自动根据传来的值设置值
        /// </summary>
        /// <param name="blackboard">将要改变的黑板值</param>
        /// <param name="value">值</param>
        //public void SetBlackBoardValue<T>(Blackboard blackboard, T value)
        //{
        //   //blackboard.Set<value>(this.BBKey, value);
        //}

        /// <summary>
        /// 自动将一个黑板的对应key的value设置到另一个黑板上
        /// </summary>
        /// <param name="oriBB">数据源黑板</param>
        /// <param name="desBB">目标黑板</param>
        //public void SetBBValueFromThisBBValue(Blackboard oriBB, Blackboard desBB)
        //{
        //    switch (this.NP_BBValueType)
        //    {
        //        case "System.String":
        //            desBB.Set(this.BBKey, oriBB.Get<string>(BBKey));
        //            break;
        //        case "System.Single":
        //            desBB.Set(this.BBKey, oriBB.Get<float>(BBKey));
        //            break;
        //        case "System.Int32":
        //            desBB.Set(this.BBKey, oriBB.Get<int>(BBKey));
        //            break;
        //        case "System.Int64":
        //            desBB.Set(this.BBKey, oriBB.Get<long>(BBKey));
        //            break;
        //        case "System.Boolean":
        //            desBB.Set(this.BBKey, oriBB.Get<bool>(BBKey));
        //            break;
        //        case "System.Collections.Generic.List`1[System.Int64]":
        //            //因为List是引用类型，所以这里要做一下特殊处理，如果要设置的值为0元素的List，就Clear一下，而且这个东西也不会用来做为黑板条件，因为它没办法用来对比
        //            //否则就拷贝全部元素
        //            List<long> oriList = oriBB.Get<List<long>>(this.BBKey);
        //            List<long> desList = desBB.Get<List<long>>(this.BBKey);
        //            if (oriList.Count == 0)
        //            {
        //                desList.Clear();
        //            }
        //            else
        //            {
        //                desList.Clear();
        //                foreach (var item in oriList)
        //                {
        //                    desList.Add(item);
        //                }
        //            }

        //            break;
        //        case "System.Numerics.Vector3":
        //            desBB.Set(this.BBKey, oriBB.Get<Vector3>(BBKey));
        //            break;
        //    }
        //}
    }
}