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

        public IBuffSystem CreatBuffSystem()
        {
           return buffSystemManager.CreatBuffSystem();
        }
    }
}


