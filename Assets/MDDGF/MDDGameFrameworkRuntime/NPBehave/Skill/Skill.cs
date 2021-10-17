using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework.Runtime
{
    public class Skill 
    {
        private Root m_MainRoot;

        private Dictionary<long, Root> m_ChildrenRoot;

        private object m_Owner;


        public void SetRootNode(Root root)
        {
            m_MainRoot = root;
        }

        public Blackboard GetBlackboard()
        {
            return m_MainRoot.Blackboard;
        }

        public void Start()
        {
            m_MainRoot.Start();
        }
    }
}


