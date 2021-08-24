using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework
{
    internal sealed class BuffSystemManager: MDDGameFrameworkModule,IBuffSystemManager
    {
        private readonly Dictionary<string, BuffSystem> m_BuffSystems;

        public BuffSystemManager()
        {
            m_BuffSystems = new Dictionary<string, BuffSystem>();
        }


        internal override void Shutdown()
        {
            
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_BuffSystems == null)
            {
                return;
            }


            foreach(var bufsystem in m_BuffSystems)
            {
                bufsystem.Value.OnUpdate(elapseSeconds,realElapseSeconds);
            }
        }

        public IBuffSystem CreatBuffSystem()
        {
            BuffSystem buff = new BuffSystem();

            m_BuffSystems.Add("1",buff);

            return buff;
        }
    }       
}




