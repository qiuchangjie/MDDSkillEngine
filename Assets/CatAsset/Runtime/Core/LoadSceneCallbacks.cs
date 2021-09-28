using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatAsset
{
    public class LoadSceneCallbacks
    {
        private readonly LoadSceneSuccessCallback loadSceneSuccessCallback;

        public LoadSceneCallbacks(LoadSceneSuccessCallback loadSceneSuccessCallback)
        {
            this.loadSceneSuccessCallback = loadSceneSuccessCallback;
        }

        /// <summary>
        /// 获取加载资源成功回调函数。
        /// </summary>
        public LoadSceneSuccessCallback LoadSceneSuccessCallback
        {
            get
            {
                return loadSceneSuccessCallback;
            }
        }
    }

}

