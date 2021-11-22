using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class NP_BlackboardConditionNode : NP_DecoratorNodeBase
    {
        protected override void Init()
        {
            base.Init();
            nodeType = NodeType.Decorator;
        }

        public override string Name => "NP_BlackboardCondition";

       
        public NP_BlackBoardConditionNodeData data = new NP_BlackBoardConditionNodeData();

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }       
    }
}



