
using MDDGameFramework.Runtime;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework;
using UnityEngine.InputSystem;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedureTimeScaleScene : MDDProcedureBase
    {

        int mainid = 0;
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            mainid = (int)Game.UI.OpenUIForm(UIFormId.Main);

            Game.Select.isWork = true;
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
           
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            Game.Select.isWork = false;
            Game.UI.CloseUIForm(mainid);

        }
    }
}
