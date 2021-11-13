using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework.Runtime
{

    public class NP_SequenceNodeData : NP_NodeDataBase
    {
        Sequence m_Sequence;

        public override Composite CreateComposite(Node[] node)
        {
            this.m_Sequence = Sequence.Create(node);
            return this.m_Sequence;
        }

        public override Node NP_GetNode()
        {
            return this.m_Sequence;
        }
    }
}


