
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework;
using OpenUIFormSuccessEventArgs = MDDGameFramework.Runtime.OpenUIFormSuccessEventArgs;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedurePreload : MDDProcedureBase
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

            m_LoadedFlag.Clear();

            PreloadResources();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {

            Game.Event.Unsubscribe(LoadDataTableSuccessEventArgs.EventId, OnLoadDataTableSuccess);
            Game.Event.Unsubscribe(LoadDataTableFailureEventArgs.EventId, OnLoadDataTableFailure);

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

            if (Game.Procedure.procedureType == ProcedureType.Game)
            {
                Game.Procedure.GetFsm().Blackboard.Set<VarInt32>("NextSceneId", 1);
            }
            else
            {
                ChangeState<ProcedureSkillEditor>(procedureOwner);
            }
        }


        private void PreloadResources()
        {
            // Preload data tables
            foreach (string dataTableName in DataTableNames)
            {
                LoadDataTable(dataTableName);
            }

            LoadFont("Vegur-Regular");

            //提前加载技能行为树资源
            //Game.NPBehave.GetHelper().PreLoad();
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
         
            Log.Info("Load data table '{0}' OK.", ne.DataTableAssetName);
        }    

        private void LoadFont(string fontName)
        {
            m_LoadedFlag.Add(Utility.Text.Format("Font.{0}", fontName), false);
            Game.Resource.LoadAsset(AssetUtility.GetFontAsset(fontName), Constant.AssetPriority.FontAsset, new LoadAssetCallbacks(
                (assetName, asset, duration, userData) =>
                {
                    m_LoadedFlag[Utility.Text.Format("Font.{0}", fontName)] = true;
                    UGuiForm.SetMainFont((Font)asset);
                    Log.Info("Load font '{0}' OK.", fontName);
                },

                (assetName, status, errorMessage, userData) =>
                {
                    Log.Error("Can not load font '{0}' from '{1}' with error message '{2}'.", fontName, assetName, errorMessage);
                }));
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
