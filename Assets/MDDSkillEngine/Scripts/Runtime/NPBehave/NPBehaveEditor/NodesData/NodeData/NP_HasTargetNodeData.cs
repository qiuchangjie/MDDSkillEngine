using Sirenix.OdinInspector;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections.Generic;

namespace MDDSkillEngine
{
    [HideLabel]
    public class NP_HasTargetNodeData : NP_NodeDataBase
    {
        NPHasTarget m_NPHasTarget;

        [BoxGroup("黑板条件节点配置")]
        [LabelText("终止条件")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;
    
        public override Decorator CreateDecoratorNode(object m_object, NP_Tree runtimeTree, Node node)
        {           
            this.m_NPHasTarget = NPHasTarget.Create( Stop, node);
            return this.m_NPHasTarget;
        }

        public override Node NP_GetNode()
        {
            return this.m_NPHasTarget;
        }
    }
}


