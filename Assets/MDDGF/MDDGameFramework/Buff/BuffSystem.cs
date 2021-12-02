using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework
{
    internal sealed class BuffSystem : BuffSystemBase,IBuffSystem,IReference
    {
        private object m_Owner;
       //private readonly MDDGameFrameworkLinkedList<BuffBase> buffs;
        private readonly Dictionary<string, BuffBase> buffs;
        private readonly List<BuffBase> m_TempBuffs;
        private BuffBase currentNode;


        public object Owner
        {
            get { return m_Owner; }
        }

        public BuffSystem()
        {
            buffs = new Dictionary<string, BuffBase>();
            m_Owner = null;
            currentNode = null;
            m_TempBuffs = new List<BuffBase>();
        }
      
        public void RemoveBuff()
        {
            currentNode=null;

            ReferencePool.EnableStrictCheck = true;

            ReferencePool.Release(currentNode);
        }

        internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (buffs.Count == 0)
            {
                return;
            }

            m_TempBuffs.Clear();

            foreach (var v in buffs)
            {
                m_TempBuffs.Add(v.Value);
            }

            foreach (var v in m_TempBuffs)
            {
                v.OnUpdate(this,elapseSeconds,realElapseSeconds);
            }

        }

        internal void Finish(string bufName)
        {
            buffs[bufName].OnFininsh(this);

            ReferencePool.Release(buffs[bufName]);

            buffs.Remove(bufName);
        }

        internal override void Shutdown()
        {
            ReferencePool.Release(this);
        }

        public void RemoveBuff(int bufID)
        {
            //buffs.Remove(bufID);

            ReferencePool.EnableStrictCheck = true; 

            ReferencePool.Release(currentNode);
        }

        public bool HasBuff(int bufID)
        {
            throw new System.NotImplementedException();
        }

        public void ClearBuff()
        {
            throw new System.NotImplementedException();
        }

        internal override void AddBuff(string buffName, object from)
        {
            if (buffs.TryGetValue(buffName, out BuffBase buff))
            {
                buff.OnRefresh(this);
                return;
            }

            buffs.Add(buffName, BuffFactory.AcquireBuff(buffName, m_Owner, from));
            buffs[buffName].OnExecute(this);
        }

        public static BuffSystem Create(object owner)
        {
            if (owner == null)
            {
                throw new MDDGameFrameworkException("FSM owner is invalid.");
            }

            BuffSystem buffSystem = ReferencePool.Acquire<BuffSystem>();
            buffSystem.m_Owner = owner;

            return buffSystem;
        }

        public void Clear()
        {
            buffs.Clear();
            m_Owner = null;
            currentNode = null;
            m_TempBuffs.Clear();
        }
    }
}
