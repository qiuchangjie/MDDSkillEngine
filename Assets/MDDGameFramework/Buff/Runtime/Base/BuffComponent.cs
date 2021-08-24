using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public sealed class BuffComponent : MDDGameFrameworkComponent
    {
        IBuffSystemManager buffSystemManager;

        protected override void Awake()
        {
            base.Awake();

            buffSystemManager = MDDGameFrameworkEntry.GetModule<IBuffSystemManager>();
        }

        public IBuffSystem CreatBuffSystem()
        {
           return buffSystemManager.CreatBuffSystem();
        }
    }
}


