using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace CatAsset
{
    /// <summary>
    /// 加载场景的任务
    /// </summary>
    public class LoadSceneTask : LoadAssetTask
    {
        protected LoadSceneCallbacks loadSceneCallbacks;


        public LoadSceneTask(TaskExcutor owner, string name, int priority, Action<object> completed, object userData, LoadSceneCallbacks loadSceneCallbacks = null, object euserData = null) : base(owner, name, priority, completed, userData)
        {
            this.loadSceneCallbacks = loadSceneCallbacks;
        }

        protected override void LoadAsync()
        {
            asyncOp = SceneManager.LoadSceneAsync(Name, LoadSceneMode.Additive);
        }

        protected override void LoadDone()
        {
            //场景加载完毕
            Completed?.Invoke(null);

            if (loadSceneCallbacks != null)
            {
                loadSceneCallbacks.LoadSceneSuccessCallback(assetInfo.ManifestInfo.AssetName, 0, userData);
            }

            Debug.Log("场景加载完毕：" + Name);
            return;
        }

     
    }
}

