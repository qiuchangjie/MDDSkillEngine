using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework
{
    public class Buff : BuffBase
    {
        public override void OnInit(IBuffSystem buffSystem, BuffDatabase buffData)
        {
           
        }

        public override void OnUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(buffSystem, elapseSeconds, realElapseSeconds);
            Debug.LogError("buff系统开始运转啦");
        }

        public override void OnExecute(IBuffSystem buffSytem)
        {
           
        }

        public override void OnFininsh(IBuffSystem buffSystem)
        {
            
        }

       
    }
}


