using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatAsset
{
    public class UnloadSceneCallbacks
    {
        private readonly UnloadSceneSuccessCallback unloadSceneSucessCallbacks;

        public UnloadSceneCallbacks(UnloadSceneSuccessCallback unloadSceneSucessCallbacks)
        {
            this.unloadSceneSucessCallbacks = unloadSceneSucessCallbacks;
        }

        /// <summary>
        /// 获取加载资源成功回调函数。
        /// </summary>
        public UnloadSceneSuccessCallback UnloadSceneSucessCallbacks
        {
            get
            {
                return unloadSceneSucessCallbacks;
            }
        }
    }

}

