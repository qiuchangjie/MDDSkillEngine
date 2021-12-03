using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework.Runtime;
using MDDGameFramework;
using System.Threading.Tasks;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedureMDDSkillFactory : ProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            Game.UI.GetUIForm(UIFormId.LoadingForm).Close();

            //Game.Resource.LoadAsset(AssetUtility.GetSkillAsset("112"), new LoadAssetCallbacks(LoadAssetCallbacksSucess));

            Game.NPBehave.GetHelper().PreLoad();

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

                Game.Entity.ShowEnemy(new EnemyData(Game.Entity.GenerateSerialId(), 10001)
                {
                    Position = new Vector3(2f, 0f, 0f),
                });
            }



          


        }

      

        //private void LoadAssetCallbacksSucess(string assetName, object asset, float duration, object userData)
        //{
        //    Log.Error("加载成功：{0}", assetName);
        //}

    }
}


