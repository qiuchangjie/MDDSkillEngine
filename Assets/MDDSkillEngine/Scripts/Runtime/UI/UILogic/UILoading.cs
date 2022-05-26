using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class UILoading : UGuiForm
    {
        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            OpenFadeOverAction = ChangeScene;
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            Log.Info("{0}ui关闭name：{1}", LogConst.UI, Name);
        }

        private void ChangeScene(bool b)
        {
            Game.Procedure.GetFsm().Blackboard.Set<VarBoolean>(typeof(ProcedureChangeScene).Name,true);
        }
    }

}

