using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class NP_AllTrueNode : NP_DecoratorNodeBase
    {
        public override string Name => "卡尔技能召唤条件判定节点";


        public NP_AllTrueNodeData data = new NP_AllTrueNodeData();

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }
    }
}



