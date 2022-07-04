using MDDGameFramework.Runtime;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedureLogin : MDDProcedureBase
    {

        int? formID;

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
        }
        

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            formID = Game.UI.OpenUIForm(UIFormId.Login);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            Game.UI.CloseUIForm((int)formID);
        }
    }
}
