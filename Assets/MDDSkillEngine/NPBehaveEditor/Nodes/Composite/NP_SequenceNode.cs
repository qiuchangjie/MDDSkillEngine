using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace NPBehave.node
{
    public class NP_SequenceNode : NP_CompositeNodeBase
    {
        public override string Name => "Sequence";
  
        public Sequence m_SequenceNode;

        public override Composite CreateCompositeNode(NP_Tree runtimeTree, Node[]node)
        {
            this.m_SequenceNode = new Sequence(node);
            return this.m_SequenceNode;
        }

        public override Node NP_GetNode()
        {
            return this.m_SequenceNode;
        }
    }
}



