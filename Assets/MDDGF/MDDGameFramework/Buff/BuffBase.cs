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


        /// <summary>
        /// buff初始化
        /// </summary>
        /// <param name="buffSystem">归属的buff系统</param>
        /// <param name="Target">buff的目标</param>
        /// <param name="From">释放的buff的实体</param>
        /// <param name="buffDatabase">basedata</param>
        /// <param name="userData">用户自定义数据</param>
        public virtual void OnInit(IBuffSystem buffSystem, object Target, object From, BuffDatabase buffDatabase = null, object userData = null)
        {
            m_Target = Target;
            m_From = From;
            buffData = buffDatabase;
        }

        /// <summary>
        /// buff执行
        /// </summary>
        /// <param name="buffSytem"></param>
        public abstract void OnExecute(IBuffSystem buffSytem);

        /// <summary>
        /// buff轮询
        /// </summary>
        /// <param name="buffSystem"></param>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        public virtual void OnUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            buffData.PassDuration += elapseSeconds;
            buffData.AccumulateDuration += elapseSeconds;

            if (buffData.Duration == -1)
            {
                //持续时间为-1则默认为永久buff
            }
            else if (buffData.Duration <= buffData.PassDuration)
            {
                BuffSystem system = (BuffSystem)buffSystem;
                system.Finish(this);
            }
        }

        /// <summary>
        /// buff轮询
        /// </summary>
        /// <param name="buffSystem"></param>
        /// <param name="elapseSeconds"></param>
        /// <param name="realElapseSeconds"></param>
        public virtual void OnFixedUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
           
        }

        /// <summary>
        /// buff结束
        /// </summary>
        /// <param name="buffSystem"></param>
        public virtual void OnFininsh(IBuffSystem buffSystem)
        {
            buffData.PassDuration = 0f;
        }

        /// <summary>
        /// buff刷新
        /// </summary>
        /// <param name="buffSystem"></param>
        public virtual void OnRefresh(IBuffSystem buffSystem) { }



        public virtual void Clear()
        {
            ReferencePool.Release(buffData);
            buffData = null;
        }
    }
}


