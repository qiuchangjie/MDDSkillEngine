using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public abstract class BuffBase<T>  where T: BuffBase<T>
    {
        public BuffDatabase buffData;

        public abstract void OnInit(int bufId);

        public abstract void OnExecute();

        public virtual void OnUpdate() { }

        public abstract void OnFininsh();

        public virtual void OnRefresh() { }
       
    }
}


