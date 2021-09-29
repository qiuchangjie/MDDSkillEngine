using MDDGameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDSkillEngine
{
    public class Buff : BuffBase
    {
        public override void OnInit(IBuffSystem buffSystem)
        {
           
        }

        public override void OnExecute(IBuffSystem buffSytem, object target, object from)
        {
            this.buffData.target = target;
            this.buffData.from = from;

            ((GameObject)target).transform.position += new Vector3(0, 10, 0);
            ((GameObject)from).transform.position += new Vector3(0, 100, 0);
        }

        public override void OnUpdate(IBuffSystem buffSystem, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(buffSystem, elapseSeconds, realElapseSeconds);
            Debug.LogError("buff系统开始运转啦");
        }


        public override void Clear()
        {
          
            Debug.LogError("clear");
        }

        public override void OnFininsh(IBuffSystem buffSystem)
        {
            
        }

       
    }
}


