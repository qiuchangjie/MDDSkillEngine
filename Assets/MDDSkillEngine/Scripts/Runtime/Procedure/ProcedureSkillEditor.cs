
using MDDGameFramework.Runtime;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedureSkillEditor : ProcedureBase
    {
      
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);         
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);        
        }      
    }
}
