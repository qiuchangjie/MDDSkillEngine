using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public abstract class BuffBase 
    {
        public BuffBase()
        {
            
        }

        public BuffDatabase buffData;

        public object target;

        public object from;

        public abstract void OnInit(IBuffSystem buffSystem,BuffDatabase buffData);

        public abstract void OnExecute(IBuffSystem buffSytem);

        public virtual void OnUpdate(IBuffSystem buffSystem,float elapseSeconds, float realElapseSeconds) { }

        public abstract void OnFininsh(IBuffSystem buffSystem);

        public virtual void OnRefresh(IBuffSystem buffSystem) { }
       
    }
}


