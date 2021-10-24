using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework.Runtime
{
    public class NP_RootNodeData : NP_NodeDataBase
    {
        private Root m_Root;

        public override Decorator CreateDecoratorNode(object m_object,NP_Tree runtimeTree, Node node)
        {
            this.m_Root =Root.Create(node);

            return this.m_Root;
        }

        public override Node NP_GetNode()
        {
            return this.m_Root;
        }
    }

}

