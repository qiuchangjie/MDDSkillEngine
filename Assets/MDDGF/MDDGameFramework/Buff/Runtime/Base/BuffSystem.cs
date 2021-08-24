﻿using System.Collections;
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

        public void AddBuff(int bufID)
        {
            if (buffs.Count == 0)
            {
                currentNode = buffs.AddFirst(BuffFactory.AcquireBuff("Buff"));
            }
            else
            {
                currentNode = buffs.AddAfter(currentNode,BuffFactory.AcquireBuff("Buff"));
            }
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