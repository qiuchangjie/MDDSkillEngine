using MDDGameFramework;
using MDDGameFramework.Runtime;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;

namespace MDDSkillEngine
{
    [Procedure]
    public abstract class MDDProcedureBase : ProcedureBase
    {
        protected ProcedureOwner procedureOwner;

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
            this.procedureOwner = procedureOwner;
            procedureOwner.SetData<VarBoolean>(GetType().Name, false);

            Log.Info("{0}设置默认状态黑板变量{1}", LogConst.FSM, GetType().Name);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            procedureOwner.SetData<VarBoolean>(GetType().Name, false);
        }
    }
}
