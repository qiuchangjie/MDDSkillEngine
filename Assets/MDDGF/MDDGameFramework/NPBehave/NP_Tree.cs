using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public abstract class NP_Tree
    {
        private Root m_MainRoot;
        private List<Node> node_Tree;

        public IEntity Owner
        {
            get
            {
              return m_MainRoot.Owner;
            }
        }

        public Root Root
        {
            get
            {
                return m_MainRoot;
            }
        }

        public Blackboard Blackboard
        {
            get
            {
                return m_MainRoot.Blackboard;
            }
        }

        public abstract void Init(Root m_MainRoot);

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

        public virtual void Clear()
        {
            for (int i = 0; i < node_Tree.Count; i++)
            {
                ReferencePool.Release(node_Tree[i]);
            }

            node_Tree = null;
            m_MainRoot = null;
        }
    }
}


