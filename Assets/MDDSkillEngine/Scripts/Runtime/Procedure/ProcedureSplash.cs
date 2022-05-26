
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedureSplash : MDDProcedureBase
    {
        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            // TODO: 这里可以播放一个 Splash 动画
            // ...

            if (Game.Base.EditorResourceMode)
            {
                // 编辑器模式
                Log.Info("编辑器资源模式.");
                ChangeState<ProcedurePreload>(procedureOwner);
            }
            else if (Game.Resource.ResourceMode == ResourceMode.Package)
            {
                // 单机模式
                Log.Info("AB包资源模式.");
                ChangeState<ProcedureInitResources>(procedureOwner);
                      
        }
    }


}
}
