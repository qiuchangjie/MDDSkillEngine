using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using MDDGameFramework;
using System;

namespace MDDSkillEngine
{
    [ShowOdinSerializedPropertiesInInspector]
    [CreateAssetMenu(fileName = "NewNPBehaveGraph", menuName = "NewNPBehaveGraph/test")]
    public class NPBehaveGraph : NodeGraph
    {
        [InfoBox("这是这个NPBehaveCanvas的所有黑板数据\n键为string，值为NP_BBValue子类\n如果要添加新的黑板数据类型，请参照BBValues文件夹下的定义")]
        [Title("黑板数据", TitleAlignment = TitleAlignments.Centered)]
        [LabelText("黑板内容")]
        [BoxGroup]
        [DictionaryDrawerSettings(KeyLabel = "键(string)", ValueLabel = "值(Variable)")]      
        public Dictionary<string,Variable> BBValues = new Dictionary<string, Variable>();


    }
}

