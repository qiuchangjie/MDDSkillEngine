﻿using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedureChangeScene : MDDProcedureBase
    {
        private const int MenuSceneId = 1;

        private bool m_ChangeToMenu = false;
        private bool m_IsChangeSceneComplete = false;
        private int m_BackgroundMusicId = 0;

        private float m_Druation = 0f;

        private bool isOpen = false;

        public  bool UseNativeDialog
        {
            get
            {
                return false;
            }
        }

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);
            procedureOwner.AddObserver("NextSceneId", ChangeSceneObserving);
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Game.Input.Control.Dispose();
            LoadScene().Start();
        }

        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            Game.Input.Control.Enable();

            Game.Event.Unsubscribe(MDDGameFramework.Runtime.LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            Game.Event.Unsubscribe(MDDGameFramework.Runtime.LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            Game.Event.Unsubscribe(MDDGameFramework.Runtime.LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            Game.Event.Unsubscribe(MDDGameFramework.Runtime.LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);
            Game.UI.GetUIForm(UIFormId.LoadingForm).Close(false);
            isOpen = false;
            m_Druation = 0f;
            base.OnLeave(procedureOwner, isShutdown);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_Druation <= 4f)
            {
                m_Druation += elapseSeconds;
                return;
            }

            if (!m_IsChangeSceneComplete)
            {
                return;
            }


            if (procedureOwner.GetData<VarInt32>("NextSceneId").Value == 2)
            {
                ChangeState<ProcedureMDDSkillFactory>(procedureOwner);
            }
            else if (procedureOwner.GetData<VarInt32>("NextSceneId").Value == 4)
            {
                ChangeState<ProcedureTimeScaleScene>(procedureOwner);
            }
            else if (procedureOwner.GetData<VarInt32>("NextSceneId").Value == 3)
            {
                ChangeState<ProcedureKearSkillDemo>(procedureOwner);
            }
            else if (procedureOwner.GetData<VarInt32>("NextSceneId").Value == 1)
            {
                ChangeState<ProcedureLogin>(procedureOwner);
            }


        }

        protected void ChangeSceneObserving(Blackboard.Type type, Variable newValue)
        {
            if (type == Blackboard.Type.CHANGE || type == Blackboard.Type.ADD)
            {
                ChangeState(procedureOwner, GetType());
            }
        }



        private void OnLoadSceneSuccess(object sender, GameEventArgs e)
        {
            MDDGameFramework.Runtime.LoadSceneSuccessEventArgs ne = (MDDGameFramework.Runtime.LoadSceneSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' OK.", ne.SceneAssetName);

            //if (m_BackgroundMusicId > 0)
            //{
            //    Game.Sound.PlayMusic(m_BackgroundMusicId);
            //}

            m_IsChangeSceneComplete = true;
        }

        private void OnLoadSceneFailure(object sender, GameEventArgs e)
        {
            MDDGameFramework.Runtime.LoadSceneFailureEventArgs ne = (MDDGameFramework.Runtime.LoadSceneFailureEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Error("Load scene '{0}' failure, error message '{1}'.", ne.SceneAssetName, ne.ErrorMessage);
        }

        private void OnLoadSceneUpdate(object sender, GameEventArgs e)
        {
            MDDGameFramework.Runtime.LoadSceneUpdateEventArgs ne = (MDDGameFramework.Runtime.LoadSceneUpdateEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' update, progress '{1}'.", ne.SceneAssetName, ne.Progress.ToString("P2"));
        }

        private void OnLoadSceneDependencyAsset(object sender, GameEventArgs e)
        {
            MDDGameFramework.Runtime.LoadSceneDependencyAssetEventArgs ne = (MDDGameFramework.Runtime.LoadSceneDependencyAssetEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            Log.Info("Load scene '{0}' dependency asset '{1}', count '{2}/{3}'.", ne.SceneAssetName, ne.DependencyAssetName, ne.LoadedCount.ToString(), ne.TotalCount.ToString());
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            MDDGameFramework.Runtime.OpenUIFormSuccessEventArgs ne = (MDDGameFramework.Runtime.OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

                
            isOpen = true;
        }


        private IEnumerator LoadScene()
        {
            Game.UI.OpenUIForm(UIFormId.LoadingForm);
            m_IsChangeSceneComplete = false;
            yield return YieldHelper.WaitForSeconds(0.5f,true);
                   
            Game.Event.Subscribe(MDDGameFramework.Runtime.LoadSceneSuccessEventArgs.EventId, OnLoadSceneSuccess);
            Game.Event.Subscribe(MDDGameFramework.Runtime.LoadSceneFailureEventArgs.EventId, OnLoadSceneFailure);
            Game.Event.Subscribe(MDDGameFramework.Runtime.LoadSceneUpdateEventArgs.EventId, OnLoadSceneUpdate);
            Game.Event.Subscribe(MDDGameFramework.Runtime.LoadSceneDependencyAssetEventArgs.EventId, OnLoadSceneDependencyAsset);
            // 停止所有声音
            //Game.Sound.StopAllLoadingSounds();
            //Game.Sound.StopAllLoadedSounds();

            // 隐藏所有实体
            Game.Entity.HideAllLoadingEntities();
            Game.Entity.HideAllLoadedEntities();

            // 卸载所有场景
            string[] loadedSceneAssetNames = Game.Scene.GetLoadedSceneAssetNames();
            for (int i = 0; i < loadedSceneAssetNames.Length; i++)
            {
                Game.Scene.UnloadScene(loadedSceneAssetNames[i]);
            }

            // 还原游戏速度
            //Game.Base.ResetNormalGameSpeed();

            int sceneId = procedureOwner.GetData<VarInt32>("NextSceneId");
            //m_ChangeToMenu = sceneId == MenuSceneId;
            IDataTable<DRScene> dtScene = Game.DataTable.GetDataTable<DRScene>();
            DRScene drScene = dtScene.GetDataRow(sceneId);
            if (drScene == null)
            {
                Log.Warning("Can not load scene '{0}' from data table.", sceneId.ToString());
            }

            Game.Scene.LoadScene(AssetUtility.GetSceneAsset(drScene.AssetName), Constant.AssetPriority.SceneAsset, this);
            //m_BackgroundMusicId = drScene.BackgroundMusicId;
        }
    }
}
