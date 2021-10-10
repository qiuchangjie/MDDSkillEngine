using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework
{
    internal sealed class BuffSystemManager: MDDGameFrameworkModule,IBuffSystemManager
    {
        private readonly Dictionary<string, BuffSystem> m_BuffSystems;
        private readonly List<BuffSystem> m_tempBuffSystems;

        public BuffSystemManager()
        {
            m_BuffSystems = new Dictionary<string, BuffSystem>();
            m_tempBuffSystems = new List<BuffSystem>();
        }

        internal override void Shutdown()
        {
            
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {          
            if (m_BuffSystems.Count == 0)
            {
                return;
            }

            m_tempBuffSystems.Clear();

            foreach (var v in m_BuffSystems)
            {
                m_tempBuffSystems.Add(v.Value);
            }

            foreach (var bufsystem in m_tempBuffSystems)
            {
                bufsystem.OnUpdate(elapseSeconds,realElapseSeconds);
            }
        }

        public IBuffSystem CreatBuffSystem(string name, object owner)
        {
            BuffSystem buffSystem = BuffSystem.Create(owner);

            m_BuffSystems.Add(name,buffSystem);

            return buffSystem;
        }

        public void AddBuff(string buffSystemName,string buffName, object target, object from)
        {
            if (m_BuffSystems.TryGetValue(buffSystemName, out BuffSystem buffSystem))
            {
                buffSystem.AddBuff(buffName,from);
            }
            else
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("m_BuffSystems is null:{0}", buffSystemName));
            }
        }

      
    }       
}




