using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class NP_HasTargetNode : NP_DecoratorNodeBase
    {
        public override string Name => "判定是否存在普通攻击目标节点";


        public NP_HasTargetNodeData data = new NP_HasTargetNodeData();

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }
    }
}



