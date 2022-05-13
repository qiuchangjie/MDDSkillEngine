using MDDGameFramework;
using System;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class MDDFsmState<T> : FsmState<T> where T : class
    {
        protected override void OnInit(IFsm<T> fsm)
        {
            base.OnInit(fsm);
            fsm.SetData<VarBoolean>(GetType().Name , false);
            Log.Info("{0}设置默认状态黑板变量{1}",LogConst.Buff, GetType().Name);
        }
    }
}
