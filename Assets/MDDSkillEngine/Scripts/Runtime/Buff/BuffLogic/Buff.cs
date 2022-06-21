using MDDGameFramework;
using MDDGameFramework.Runtime;
using UnityEngine;


namespace MDDSkillEngine
{
    public class Buff : BuffBase
    {

        public override void OnExecute(IBuffSystem buffSytem)
        {           
            //订阅近战攻击击中事件
        }

        public override void OnFininsh(IBuffSystem buffSystem)
        {
            //取消近战攻击击中事件
        }

        //事件触发函数
        private void CacheEvent()
        {
            
        }

        public override void Clear()
        {
            buffData = null;
        }
    }
}


