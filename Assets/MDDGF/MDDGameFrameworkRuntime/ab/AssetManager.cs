using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MDDGameFramework.Runtime
{
    public class AssetManager
    {
        /// <summary>
        /// 单例对象
        /// </summary>
        private static AssetManager _instance = new AssetManager();

        //构造方法初始化 AllManifest
        public AssetManager()
        {
#if !UNITY_EDITOR
            //单例初始化 manifest依赖关系
            var bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "StreamingAssets"));
            AllManifest = bundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
            if (AllManifest == null)
                Debug.LogError("mainfest 异常！！？");
            //string[] depends = AllManifest.GetDirectDependencies("ui_chat");
            //Debug.Log("bundle path "  + Path.Combine(Application.streamingAssetsPath, "StreamingAssets"));
            //Debug.Log("ui_prefabs depens len : " + depends.Length);
            //foreach (var item in depends)
            //{
            //    Debug.Log("ui_prefabs depend :" + item);
            //}

#endif
        }

        //缓存已加载的AssetBundle
        public Dictionary<string, AssetBundle> AssetBundleDic = new Dictionary<string, AssetBundle>();
        //缓存已加载的AssetBundle资源名
        public Dictionary<string, string[]> AssetBundleNames = new Dictionary<string, string[]>();

        //缓存所有Manifest依赖关系
        private static AssetBundleManifest AllManifest;

        public static AssetManager Instance
        {
            get
            {
                return _instance;
            }
        }

        public static AssetBundle GetBundle(string bundleName)
        {

            if (Instance.AssetBundleDic.ContainsKey(bundleName))
            {
                Debug.Log("<color=blue>获取已加载的bundle " + bundleName + "</color>");
                return Instance.AssetBundleDic[bundleName] as AssetBundle;
            }
            else
            {
                Debug.Log("新加载 加载bundle " + bundleName);
                string path = Path.Combine(Application.streamingAssetsPath, bundleName);
                Debug.Log("GetBundle " + path);
                LoadDepend(bundleName);//引用先加载
                AssetBundle ab = AssetBundle.LoadFromFile(path);
                Instance.AssetBundleDic.Add(bundleName, ab);
                if (ab == null)
                    Debug.LogError($"{bundleName} 未加载到!? 快去查查吧");
                return ab;
            }

        }

        private static void LoadDepend(string bundleName)
        {
            if (AllManifest == null)
                Debug.LogError("AllManifest is null");
            string[] bundleList = AllManifest.GetDirectDependencies(bundleName);
            int count = 0;
            foreach (string name in bundleList)
            {
                Debug.Log($" {bundleName} size:{bundleList.Length}  ct:{count}:  间接引用 加载bundle " + name);
                count++;
                GetBundle(name);
            }
        }

        //检测当前文件在资源包中是否存在
        public static bool CheckBundleExists(string bundle, string name)
        {
#if UNITY_EDITOR
        string[] texPath = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(bundle, name);
        return texPath.Length != 0;
#else
            if (!Instance.AssetBundleNames.ContainsKey(bundle))
            {
                AssetBundle ab = GetBundle(bundle);
                string[] assetNames = ab.GetAllAssetNames();
                for (int i = 0; i < assetNames.Length; i++)
                {
                    var temp = assetNames[i].Split('/', '.');
                    assetNames[i] = temp[temp.Length - 2].ToUpper();
                }
                Instance.AssetBundleNames[bundle] = assetNames;
            }
            return Instance.AssetBundleNames[bundle].Contains(name.ToUpper());
#endif
        }

        //加载一个包体下的全部资源
        public static List<T> GetAllBundleAsset<T>(string bundle) where T : UnityEngine.Object
        {
            List<T> objList = new List<T>();
#if UNITY_EDITOR
        string[] texPath = AssetDatabase.GetAssetPathsFromAssetBundle(bundle);
        foreach (var v in texPath)
        {
            objList.Add(AssetDatabase.LoadAssetAtPath<T>(v));
        }
        return objList;
#else
            AssetBundle ab = GetBundle(bundle);
            if (ab == null)
            {
                Debug.LogError($"bundle {bundle} is null");
                return objList;
            }
            foreach (T v in ab.LoadAllAssets<T>())
            {
                objList.Add(v);
            }
            return objList;
#endif
        }

        public static T Get<T>(string buddle, string name) where T : UnityEngine.Object
        {
            //Debug.LogError("getbundle " + buddle + " " + name);
            try
            {
#if UNITY_EDITOR
            var texPath = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(buddle, name)[0];

            T obj = AssetDatabase.LoadAssetAtPath<T>(texPath);

            return obj;
#else
                AssetBundle ab = GetBundle(buddle);
                if (ab == null)
                    Debug.LogError("getbundle  ab is null " + buddle);
                var unityAsset = ab.LoadAsset<T>(name);
                return unityAsset;
#endif
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);

                return null;
            }
        }

        public static T[] GetAllsub<T>(string buddle, string name) where T : UnityEngine.Object
        {
            //Debug.Log("loadAll " + buddle +" " + name);
#if UNITY_EDITOR

        //string[] pathList = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(buddle, name);
        //for (int i = 0; i < pathList.Length; i++)
        //{
        //    Debug.Log(pathList[i]);
        //}

        var texPath = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(buddle, name)[0];

        Object[] obj = AssetDatabase.LoadAllAssetRepresentationsAtPath(texPath);

        if (typeof(T) != typeof(Object))
        {
            //返回类型转换
            T[] RetList = new T[obj.Length];
            int i = 0;

            foreach (Object o in obj)
            {
                RetList[i] = (T)o;
                i++;
            }
            return RetList;
        }
        else
            return (T[])obj;

#else
            AssetBundle ab = GetBundle(buddle);
            T[] unityAssets = ab.LoadAssetWithSubAssets<T>(name);
            return unityAssets;
#endif
        }

        /// <summary>
        /// 获取 muliti sprite 资源的子资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buddle"></param>
        /// <param name="name">muliti资源名</param>
        /// <param name="subname">子资源名</param>
        /// <returns></returns>
        public static T GetSubByName<T>(string buddle, string name, string subname) where T : UnityEngine.Object
        {
#if UNITY_EDITOR



        var texPath = AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(buddle, name)[0];

        Object[] obj = AssetDatabase.LoadAllAssetRepresentationsAtPath(texPath);




        foreach (Object o in obj)
        {
            if (o.name == subname)
            {

                return (T)o;
            }
        }

        return null;
#else
            AssetBundle ab = GetBundle(buddle);
            T[] unityAssets = ab.LoadAssetWithSubAssets<T>(name);
            foreach (Object o in unityAssets)
            {
                if (o.name == subname)
                {

                    return (T)o;
                }
            }
            return null;
#endif
        }




#if UNITY_EDITOR
    //获取指定bundle下的所有文件名
    public static string[] GetAssetBundleNames(string bundleName)
    {
        string[] assetPath = AssetDatabase.GetAssetPathsFromAssetBundle(bundleName);
        for (int i = 0; i < assetPath.Length; i++)
        {
            string[] singlePathlist = assetPath[i].Split('/');
            assetPath[i] = singlePathlist[singlePathlist.Length - 1].Split('.')[0];
        }
        return assetPath;
    }
#endif
    }
}


