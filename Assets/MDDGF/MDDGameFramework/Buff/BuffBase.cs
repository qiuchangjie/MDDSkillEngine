using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public abstract class BuffBase : IReference
    {
        private object m_Target;
        private object m_From;

        public BuffDatabase buffData;

        /// <summary>
        /// buff目标
        /// </summary>
        public object Target
        {
            get { return m_Target; }
            protected set { m_Target = value; }
        }

        /// <summary>
        /// buff释放者
        /// </summary>
        public object From
        {
            get { return m_From; }
            protected set { m_From = value; }
        }

        public abstract void OnInit(IBuffSystem buffSystem,object Target,object From,BuffDatabase buffDatabase=null);

        public abstract void OnExecute(IBuffSystem buffSytem);

        public virtual void OnUpdate(IBuffSystem buffSystem,float elapseSeconds, float realElapseSeconds) 
        {
            
        }

        public abstract void OnFininsh(IBuffSystem buffSystem);

        public virtual void OnRefresh(IBuffSystem buffSystem) { }

        public abstract void Clear();
    }
}


