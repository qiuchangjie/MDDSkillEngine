using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public abstract class NP_Tree : IReference
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

        public virtual void Init(Root m_MainRoot)
        {
            node_Tree=new List<Node>();

        }

        public void SetNodeList(List<Node> nodes)
        {
            node_Tree=nodes;
        }

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
            ReferencePool.Release(Blackboard);

            for (int i = 0; i < node_Tree.Count; i++)
            {
                ReferencePool.Release(node_Tree[i]);
            }
            
            node_Tree.Clear();
            m_MainRoot = null;
        }
    }
}


