using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework
{
    internal sealed class BuffSystem : BuffSystemBase,IBuffSystem
    {
        private readonly MDDGameFrameworkLinkedList<BuffBase> buffs;
        private readonly List<BuffBase> m_TempBuffs;
        private LinkedListNode<BuffBase> currentNode;

        public BuffSystem()
        {
            buffs = new MDDGameFrameworkLinkedList<BuffBase>();
            m_TempBuffs = new List<BuffBase>();
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
            ReferencePool.Release(currentNode.Value);
        }

        internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            m_TempBuffs.Clear();

            if (buffs.Count <= 0)
            {
                return;
            }

            foreach (var buff in buffs)
            {
                m_TempBuffs.Add(buff);
            }

            foreach (var buff in m_TempBuffs)
            {
                buff.OnUpdate(this,elapseSeconds,realElapseSeconds);
            }

        }

        internal override void Shutdown()
        {
            
        }
    }
}
