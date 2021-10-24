using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public abstract class NP_Tree
    {
        private Root m_MainRoot;

        private Dictionary<long, Root> m_ChildrenRoot;

        private object m_Owner;

        private List<Node> node_Tree;

        public virtual void SetRootNode(Root root)
        {
            m_MainRoot = root;
        }

        public virtual Blackboard GetBlackboard()
        {
            return m_MainRoot.Blackboard;
        }

        public virtual void Start()
        {
            m_MainRoot.Start();
        }
    }
}


