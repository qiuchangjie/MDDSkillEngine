using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework
{
    internal sealed class BuffSystem : BuffSystemBase,IBuffSystem
    {
        private object m_Owner;
        private readonly MDDGameFrameworkLinkedList<BuffBase> buffs;
        private LinkedListNode<BuffBase> currentNode;

        public object Owner
        {
            get { return m_Owner; }
        }

        public BuffSystem()
        {
            buffs = new MDDGameFrameworkLinkedList<BuffBase>();
        }

        public void AddBuff(int bufID,object target,object from)
        {
            if (buffs.Count == 0)
            {
                currentNode = buffs.AddFirst(BuffFactory.AcquireBuff("Buff",1));
                currentNode.Value.OnExecute(this,target,from);
                //currentNode.Value.OnInit()
            }
            else
            {
                currentNode = buffs.AddAfter(currentNode,BuffFactory.AcquireBuff("Buff",1));
                currentNode.Value.OnExecute(this,target,from);
            }
        }

        public void RemoveBuff()
        {
            currentNode.Value.Clear();

            ReferencePool.EnableStrictCheck = true;

            ReferencePool.Release(currentNode.Value);
        }

        internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (currentNode == null)
            {
                return;
            }

            foreach (var v in buffs)
            {
                v.OnUpdate(this,elapseSeconds,realElapseSeconds);
            }

        }

        internal override void Shutdown()
        {
            
        }

        public void RemoveBuff(int bufID)
        {
            currentNode.Value.Clear();

            ReferencePool.EnableStrictCheck = true;

            ReferencePool.Release(currentNode.Value);
        }

        public bool HasBuff(int bufID)
        {
            throw new System.NotImplementedException();
        }

        public void ClearBuff()
        {
            throw new System.NotImplementedException();
        }
    }
}
