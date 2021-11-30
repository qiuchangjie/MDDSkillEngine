using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using MDDGameFramework;
using System;
using MDDSkillEngine;

namespace MDDSkillEngine
{
    [ShowOdinSerializedPropertiesInInspector]
    [CreateAssetMenu(fileName = "NewNPBehaveGraph", menuName = "NewNPBehaveGraph/test")]
    public class NPBehaveGraph : NodeGraph
    {
        [InfoBox("这是这个NPBehaveGraph的所有黑板数据\n键为string，值为Variable子类\n如果要添加新的黑板数据类型，请参照Variable文件夹下的定义")]
        [Title("黑板数据", TitleAlignment = TitleAlignments.Centered)]
        [LabelText("黑板内容")]
        [BoxGroup]
        [DictionaryDrawerSettings(KeyLabel = "键(string)", ValueLabel = "值(Variable)")]      
        public Dictionary<string,Variable> BBValues = new Dictionary<string, Variable>();


        public XNode.Node GetRootNode()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                NP_NodeBase node = nodes[i] as NP_NodeBase;

                if (node.Name == "根节点")
                {
                    return node;
                }
            }
            return null;
        }
    }
}

