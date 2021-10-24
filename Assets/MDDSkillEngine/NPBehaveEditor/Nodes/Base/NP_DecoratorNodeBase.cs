using MDDGameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static XNode.Node;

namespace NPBehave.node
{
    public abstract class NP_DecoratorNodeBase : NP_NodeBase
    {
        [Input(ShowBackingValue.Unconnected, ConnectionType.Override)]
        public bool input;

        [Output(ShowBackingValue.Never, ConnectionType.Override)]
        public bool output;
    }
}

