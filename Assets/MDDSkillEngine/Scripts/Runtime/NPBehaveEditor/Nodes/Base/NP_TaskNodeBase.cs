using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;

namespace MDDSkillEngine
{
    public abstract class NP_TaskNodeBase : NP_NodeBase
    {
        protected override void Init()
        {
            nodeType = NodeType.Task;
        }

        [Input(ShowBackingValue.Unconnected, ConnectionType.Override)]
        public bool input;       
    }
}

