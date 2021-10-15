using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPBehave.node
{
    public abstract class NP_TaskNodeBase : NP_NodeBase
    {
        [Input(ShowBackingValue.Unconnected, ConnectionType.Override)]
        public bool input;       
    }
}

