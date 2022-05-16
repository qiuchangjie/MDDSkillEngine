using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework.Runtime
{
    [DisallowMultipleComponent]
    public sealed class BuffComponent : MDDGameFrameworkComponent
    {
        private IBuffSystemManager buffSystemManager;

        protected override void Awake()
        {
            base.Awake();

            buffSystemManager = MDDGameFrameworkEntry.GetModule<IBuffSystemManager>();
        }

        private void Start()
        {          
        }

        public IBuffSystem CreatBuffSystem(string name,object owner)
        {
           return buffSystemManager.CreatBuffSystem(name, owner);
        }

        public void AddBuff(string BufSystemName, string buffName, object target, object from)
        {
            buffSystemManager.AddBuff(BufSystemName, buffName,target,from);
        }

        public IBuffSystem GetBuffSystem(string name)
        {
            return buffSystemManager.GetBuffSystem(name);
        }
    }
}


