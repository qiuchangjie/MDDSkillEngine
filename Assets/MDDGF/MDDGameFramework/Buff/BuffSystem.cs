using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework
{
    internal sealed class BuffSystem : BuffSystemBase,IBuffSystem,IReference
    {
        private object m_Owner;
        private readonly List<BuffBase> buffs;
        private readonly List<BuffBase> m_TempBuffs;
        private BuffBase currentNode;


        public object Owner
        {
            get { return m_Owner; }
        }

        public BuffSystem()
        {
            buffs = new List<BuffBase>();
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
                m_TempBuffs.Add(v);
            }

            foreach (var v in m_TempBuffs)
            {
                v.OnUpdate(this,elapseSeconds,realElapseSeconds);
            }

        }

        internal void Finish(BuffBase buff)
        {
            buff.OnFininsh(this);

            ReferencePool.Release(buff);
            buffs.Remove(buff);
        }

        internal override void Shutdown()
        {
            ReferencePool.Release(this);
        }

        public void RemoveBuff(int bufID)
        {
            ReferencePool.Release(currentNode);
        }

        public bool HasBuff(int bufID)
        {
            foreach (var v in buffs)
            {
                if (v.buffData.Id == bufID)
                {
                    return true;
                }
            }

            return false;
        }

        public void ClearBuff()
        {
            throw new System.NotImplementedException();
        }

        internal override void AddBuff(string buffName, object from)
        {
            BuffBase buff = BuffFactory.AcquireBuff(buffName, m_Owner, from);
            buffs.Add(buff);
            buff.OnExecute(this);
        }

        public static BuffSystem Create(object owner)
        {
            if (owner == null)
            {
                throw new MDDGameFrameworkException("Buff owner is invalid.");
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
