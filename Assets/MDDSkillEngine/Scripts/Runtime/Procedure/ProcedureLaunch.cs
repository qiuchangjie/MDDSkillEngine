using MDDGameFramework.Runtime;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedureLaunch : ProcedureBase
    {

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);

            Log.Error("11111111111111111111111111111111111111");
        }
        
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            Log.Info("开始游戏");
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // 运行一帧即切换到 Splash 展示流程
            ChangeState<ProcedureSplash>(procedureOwner);
        }
    }
}
