using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace NPBehave.node
{
    public class NP_RootNode : NP_NodeBase
    {
        protected override void Init()
        {
            base.Init();
            nodeType = NodeType.Decorator;
        }

        public override string Name => "根节点";

        [Output(ShowBackingValue.Never, ConnectionType.Override)]
        public bool output;

        public NP_RootNodeData data=new NP_RootNodeData();

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }

        //public override Decorator CreateDecoratorNode(NP_Tree runtimeTree, Node node)
        //{
        //    this.m_Root = new Root(node);
        //    return this.m_Root;
        //}

        //public override Node NP_GetNode()
        //{
        //    return this.m_Root;
        //}
    }
}



