using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;

namespace MDDSkillEngine
{
    public abstract class NP_CompositeNodeBase : NP_NodeBase
    {
        protected override void Init()
        {
            nodeType = NodeType.Composite;
        }

        [Input(ShowBackingValue.Unconnected, ConnectionType.Override)]
        public bool input;

        [Output(ShowBackingValue.Never, ConnectionType.Multiple)]
        public bool output;

    }
}

