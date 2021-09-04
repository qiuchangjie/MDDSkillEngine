using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatAsset
{
    public class LoadAssetCallbacks 
    {
        private readonly LoadAssetSuccessCallback loadAssetSuccessCallback;

        public LoadAssetCallbacks(LoadAssetSuccessCallback loadAssetSuccessCallback)
        {
            this.loadAssetSuccessCallback = loadAssetSuccessCallback;
        }

        /// <summary>
        /// 获取加载资源成功回调函数。
        /// </summary>
        public LoadAssetSuccessCallback LoadAssetSuccessCallback
        {
            get
            {
                return loadAssetSuccessCallback;
            }
        }
    }

}

