using System;
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


        public object Owner
        {
            get { return m_Owner; }
        }

        public BuffSystem()
        {
            buffs = new List<BuffBase>();
            m_Owner = null;
            m_TempBuffs = new List<BuffBase>();
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
            foreach (var v in buffs)
            {
                if (v.buffData.Id == bufID)
                {
                    v.OnFininsh(this);
                    ReferencePool.Release(v);
                    buffs.Remove(v);
                }
            }
        }

        public void RemoveBuff(BuffBase buf)
        {
            buf.OnFininsh(this);
            buffs.Remove(buf);
            ReferencePool.Release(buf);
        }

        public void RemoveBuff(object from)
        {
            foreach (var v in buffs)
            {
                if (v.From == from)
                {
                    v.OnFininsh(this);
                    ReferencePool.Release(v);
                    buffs.Remove(v);
                }
            }
        }

        public void Removebuff(object from, Type type)
        {
            foreach (var v in buffs)
            {
                if (v.From == from)
                {
                    if (v.GetType() == type)
                    {
                        v.OnFininsh(this);
                        ReferencePool.Release(v);
                        buffs.Remove(v);
                    }                 
                }
            }
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

        public bool HasBuff(string name)
        {
            foreach (var v in buffs)
            {
                if (v.buffData.Name == name)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasBuff(Type type)
        {
            foreach (var v in buffs)
            {
                if (v.GetType() == type)
                {
                    return true;
                }
            }

            return false;
        }

        public BuffBase GetBuff(string name)
        {
            foreach (var v in buffs)
            {
                if (v.buffData.Name == name)
                {
                    return v;
                }
            }

            return null;
        }

        public void ClearAllBuff()
        {
            m_TempBuffs.Clear();

            foreach (var v in buffs)
            {
                RemoveBuff(v);
            }

            buffs.Clear();
        }

        internal override void AddBuff(string buffName, object from,object userData=null)
        {
            BuffBase buf = GetBuff(buffName);
            if (buf != null)
            {
                if (buf.buffData.CanOverlying)
                {
                    buf.OnRefresh(this);
                }
                else
                {
                    BuffBase buff = BuffFactory.AcquireBuff(buffName, m_Owner, from, userData);
                    buffs.Add(buff);
                    buff.OnExecute(this);
                }
            }
            else
            {
                BuffBase buff = BuffFactory.AcquireBuff(buffName, m_Owner, from, userData);
                buffs.Add(buff);
                buff.OnExecute(this);
            }
           
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
            foreach (var item in buffs)
            {
                ReferencePool.Release(item);
            }

            buffs.Clear();
            m_Owner = null;
            m_TempBuffs.Clear();
        }     
    }
}
