using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public abstract class BuffBase : IReference
    {
        public BuffBase()
        {
            
        }

        public BuffDatabase buffData;

        public abstract void OnInit(IBuffSystem buffSystem);

        public abstract void OnExecute(IBuffSystem buffSytem,object target, object from);

        public virtual void OnUpdate(IBuffSystem buffSystem,float elapseSeconds, float realElapseSeconds) { }

        public abstract void OnFininsh(IBuffSystem buffSystem);

        public virtual void OnRefresh(IBuffSystem buffSystem) { }

        public abstract void Clear();
    }
}


