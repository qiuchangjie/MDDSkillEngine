using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework.Runtime;
using MDDGameFramework;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedureMDDSkillFactory : ProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            Game.UI.GetUIForm(UIFormId.LoadingForm).Close();

            Log.Info("成功进入训练场景");
        }


        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (Input.GetKeyDown(KeyCode.C))
            {
                Game.Entity.ShowPlayer(new PlayerData(Game.Entity.GenerateSerialId(), 10000)
                {
                    Position = new Vector3(0f, 0f, 0f),                  
                });
            }


        }
    }
}


