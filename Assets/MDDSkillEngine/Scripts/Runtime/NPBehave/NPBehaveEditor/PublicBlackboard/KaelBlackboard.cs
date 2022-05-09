using MDDGameFramework;
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [CreateAssetMenu(fileName = "KealBB", menuName = "公共黑板/kealBB")]
    [Serializable]
    public class KaelBlackboard : SerializedScriptableObject
    {
        [InfoBox("角色级公共黑板数据（卡尔）")]
        [Title("黑板数据", TitleAlignment = TitleAlignments.Centered)]
        [LabelText("黑板内容")]
        [BoxGroup]
        [DictionaryDrawerSettings(KeyLabel = "键(string)", ValueLabel = "值(Variable)")]
        public Dictionary<string, Variable> BBValues = new Dictionary<string, Variable>();
    }
}


