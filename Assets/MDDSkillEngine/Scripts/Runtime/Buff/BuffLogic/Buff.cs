using MDDGameFramework;
using MDDGameFramework.Runtime;
using UnityEngine;


namespace MDDSkillEngine
{
    public class MDDBuff : BuffBase
    {
        public override void OnExecute(IBuffSystem buffSytem)
        {
            
        }

        public override void OnUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(buffSystem, elapseSeconds, realElapseSeconds);
        }

        public override void OnFixedUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            base.OnFixedUpdate(buffSystem, elapseSeconds, realElapseSeconds);
        }
    }
}


