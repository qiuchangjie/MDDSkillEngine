using MDDGameFramework;
using System;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public abstract class MDDFsmState<T> : FsmState<T> where T : class
    {
        protected IFsm<T> Fsm; 

        protected override void OnInit(IFsm<T> fsm)
        {
            base.OnInit(fsm);
            Fsm = fsm;
            fsm.SetData<VarBoolean>(GetType().Name , false);
            fsm.AddObserver(GetType().Name, Observing);
            Log.Info("{0}设置默认状态黑板变量{1}",LogConst.FSM, GetType().Name);
        }

        protected override void OnLeave(IFsm<T> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            fsm.SetData<VarBoolean>(GetType().Name, false);
        }

        /// <summary>
        /// 状态跳转的依据函数
        /// </summary>
        /// <param name="type">黑板值变化类型</param>
        /// <param name="newValue">值</param>
        protected abstract void Observing(Blackboard.Type type, Variable newValue);
        
        
    }
}
