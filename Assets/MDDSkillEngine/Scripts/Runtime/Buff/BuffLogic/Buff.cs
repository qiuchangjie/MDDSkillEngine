using MDDGameFramework;
using MDDGameFramework.Runtime;
using UnityEngine;


namespace MDDSkillEngine
{
    public class Buff : BuffBase
    {
        Entity entity;

        public override void OnInit(IBuffSystem buffSystem,object target,object from, BuffDatabase buffDatabase = null,object userData = null)
        {
            Target = target;
            From = from;
        }

        public override void OnExecute(IBuffSystem buffSytem)
        {
            entity = Target as Entity;           
        }

        public override void OnUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(buffSystem, elapseSeconds, realElapseSeconds);

            if (entity != null)
            {
                entity.CachedTransform.Translate(Vector3.one * 10 * elapseSeconds, Space.World);
            }
            else
            {
                Log.Error("转化失败");
            }

            Log.Error("buff系统开始运转啦");
        }


        public override void Clear()
        {
            buffData = null;
        }

        public override void OnFininsh(IBuffSystem buffSystem)
        {
            
        }

       
    }
}


