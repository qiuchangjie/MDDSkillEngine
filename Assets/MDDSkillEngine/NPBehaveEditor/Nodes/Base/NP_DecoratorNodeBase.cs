using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

