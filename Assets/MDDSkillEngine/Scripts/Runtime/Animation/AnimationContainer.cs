using Animancer;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MDDSkillEngine
{
    public class AnimationContainer : SerializedMonoBehaviour
    {
        public string animid;

        [SerializeField]
        [DictionaryDrawerSettings(KeyLabel = "动画名", ValueLabel = "AnimationClip")]
        [ShowInInspector]
        [InfoBox("动画资源容器字典")]
        private Dictionary<string, ClipState.Transition> animDic = new Dictionary<string, ClipState.Transition>();

        ClipState.Transition transition;

        public void SetSpeed(float speed)
        {
            foreach (var item in animDic)
            {
                item.Value.Speed = speed;
            }
        }

        public ClipState.Transition GetAnimation(string animName)
        {
            ClipState.Transition anim;
            if (animDic.TryGetValue(animName, out anim))
            {
                return anim;
            }

            return null;
        }

        [Button]
        private void InitAnimDic()
        {
            //路径
            string fullPath = Application.dataPath + "/MDDSkillEngine/Animation/"+animid+"/";
            Debug.Log(fullPath);
            //获得指定路径下面的所有资源文件
            if (Directory.Exists(fullPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(fullPath);
                FileInfo[] files = dirInfo.GetFiles("*", SearchOption.AllDirectories); //包括子目录
                Debug.Log(files.Length);

                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Name.EndsWith(".anim"))
                    {
                        Debug.Log("预制体名字" + files[i].Name);
                        string path = "Assets/MDDSkillEngine/Animation/" + animid + "/" + files[i].Name;
                        Debug.Log("预制体路径" + path);
                        UnityEngine.AnimationClip obj = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.AnimationClip)) as UnityEngine.AnimationClip;
                        Debug.Log("obj的名字" + obj.name);

                        ClipState.Transition transition = new ClipState.Transition();
                        transition.Clip = obj;
                        if (animDic.ContainsKey(obj.name))
                        {

                        }
                        else
                        {
                            animDic.Add(obj.name, transition);
                        }
                        
                    }
                }
            }
            else
            {
                Debug.Log("资源路径不存在");
            }
        }
    }
}


