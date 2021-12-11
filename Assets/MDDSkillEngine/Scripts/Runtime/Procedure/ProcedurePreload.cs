
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework;
using OpenUIFormSuccessEventArgs = MDDGameFramework.Runtime.OpenUIFormSuccessEventArgs;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedurePreload : ProcedureBase
    {
        public static readonly string[] DataTableNames = new string[]
        {   
            "Player",
            "Scene",
            "UIForm",
            "Entity",
            "Buff",
            "Skill",
            "Hero"
        };

        private Dictionary<string, bool> m_LoadedFlag = new Dictionary<string, bool>();


        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            Game.Event.Subscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            Game.Event.Subscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            Game.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            m_LoadedFlag.Clear();

            PreloadResources();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {

            Game.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            Game.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);
            Game.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            foreach (KeyValuePair<string, bool> loadedFlag in m_LoadedFlag)
            {
                if (!loadedFlag.Value)
                {
                    return;
                }
            }

            procedureOwner.SetData<VarInt32>("NextSceneId",2);

            ChangeState<ProcedureChangeScene>(procedureOwner);
        }


        private void PreloadResources()
        {
            // Preload data tables
            foreach (string dataTableName in DataTableNames)
            {
                LoadDataTable(dataTableName);
            }

            //LoadLoadingUIAsset();
        }

        private void LoadDataTable(string dataTableName)
        {
            string dataTableAssetName = AssetUtility.GetDataTableAsset(dataTableName, true);
            m_LoadedFlag.Add(dataTableAssetName, false);
            Game.DataTable.LoadDataTable(dataTableName, dataTableAssetName, this);
        }

        private void OnLoadDataTableSuccess(object sender, GameEventArgs e)
        {
            LoadDataTableSuccessEventArgs ne = (LoadDataTableSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_LoadedFlag[ne.DataTableAssetName] = true;

            if (ne.DataTableAssetName == AssetUtility.GetUIFormAsset("UILoadingForm"))
            {
                LoadLoadingUIAsset();
            }

            Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
        }

        private void LoadLoadingUIAsset()
        {
            Game.UI.OpenUIForm(UIFormId.LoadingForm, this);
            m_LoadedFlag.Add(UIFormId.LoadingForm.ToString(), false);
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            MDDGameFramework.Runtime.OpenUIFormSuccessEventArgs ne = (MDDGameFramework.Runtime.OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("打开ui成功");
            Game.UI.GetUIForm(UIFormId.LoadingForm).Close(true);
            m_LoadedFlag[UIFormId.LoadingForm.ToString()] = true;
        }

        private void OnLoadDataTableFailure(object sender, GameEventArgs e)
        {
            LoadDataTableFailureEventArgs ne = (LoadDataTableFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Can not load data table '{0}' from '{1}' with error message '{2}'.", ne.DataTableAssetName, ne.DataTableAssetName, ne.ErrorMessage);
        }
    }
}
