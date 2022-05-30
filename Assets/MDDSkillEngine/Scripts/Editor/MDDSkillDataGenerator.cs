using OdinSerializer;
using Slate;
using Slate.ActionClips;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using LitJson;

namespace MDDSkillEngine
{
    public class MDDSkillDataGenerator : EditorWindow
    {
        /// <summary>
        /// 处理技能数据函数
        /// 用来导出存在slate中的timeline数据到 skilldata中
        /// </summary>
        [MenuItem("MDDSkillEngine/GeneratorSkillData")]
        public static void GeneratorSkillData()
        {
            //路径
            string fullPath = Application.dataPath + "/MDDSkillEngine/SkillRes/";
            string savePath = Application.dataPath + "/MDDSkillEngine/SkillRes/SkillTimelineRuntime/";
            Debug.Log(fullPath);
            //获得指定路径下面的所有资源文件
            if (Directory.Exists(fullPath))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(fullPath);
                FileInfo[] files = dirInfo.GetFiles("*", SearchOption.AllDirectories); //包括子目录
                Debug.Log(files.Length);
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Name.EndsWith(".prefab"))
                    {
                        Debug.Log("预制体名字" + files[i].Name);
                        string path = "Assets/MDDSkillEngine/SkillRes/SkillTimeline/" + files[i].Name;
                        Debug.Log("预制体路径" + path);
                        GameObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
                        Debug.Log("obj的名字" + obj.name);

                        Cutscene Data = obj.GetComponent<Cutscene>();

                        byte[] bytes = SerializationUtility.SerializeValue(HandleSkillData(Data), DataFormat.Binary);

                        File.WriteAllBytes(savePath + Data.gameObject.name+ ".bytes", bytes);



                        //通知你的编辑器 obj 改变了
                        EditorUtility.SetDirty(obj);
                        //保存修改
                        AssetDatabase.SaveAssets();
                        AssetDatabase.Refresh();
                    }
                }
            }
            else
            {
                Debug.Log("资源路径不存在");
            }

            Debug.Log("数据导出完成");
        }


        /// <summary>
        /// 返回一份slate数据中的技能导出数据
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public static SkillData HandleSkillData(Cutscene Data)
        {
            List<SkillDataBase> dataList = new List<SkillDataBase>();

            foreach (var group in Data.groups)
            {
                foreach (var track in group.tracks)
                {
                    switch (track.SkillDataType)
                    {
                        case SkillDataType.None:
                            Debug.LogError($"检测到未指定类型的技能数据{track.name}");
                            continue;
                        case SkillDataType.Effect:
                            {
                                foreach (var clip in track.clips)
                                {
                                    if (clip is EffectInstance)
                                    {
                                        EffectInstance effectInstance = (EffectInstance)clip;
                                        EffectSkillData data = new EffectSkillData();
                                        data.OnInit(effectInstance);
                                        dataList.Add(data);
                                    }
                                }
                                break;
                            }
                        case SkillDataType.Animation:
                            {
                                foreach (var clip in track.clips)
                                {
                                    if (clip is PlayAnimatorClip)
                                    {
                                        PlayAnimatorClip playAnimatorClip = (PlayAnimatorClip)clip;
                                        AnimationSkillData data = new AnimationSkillData();
                                        data.OnInit(playAnimatorClip);
                                        dataList.Add(data);
                                    }
                                }
                                break;
                            }
                        case (SkillDataType.Collider):
                            {
                                foreach (var clip in track.clips)
                                {
                                    if (clip is InstanceCollider)
                                    {
                                        InstanceCollider instanceCollider = (InstanceCollider)clip;
                                        ColliderSkillData data = new ColliderSkillData();
                                        data.OnInit(instanceCollider);
                                        dataList.Add(data);
                                    }
                                }
                                break;
                            }
                        case (SkillDataType.CD):
                            {
                                foreach (var clip in track.clips)
                                {
                                    if (clip is CD)
                                    {
                                        CD cd = (CD)clip;
                                        CDSkillData data = new CDSkillData();
                                        data.OnInit(cd);
                                        dataList.Add(data);
                                    }
                                }
                                break;
                            }
                        case (SkillDataType.InOut):
                            {
                                foreach (var clip in track.clips)
                                {
                                    if (clip is SkillFadeInOut)
                                    {
                                        SkillFadeInOut skillFadeInOut = (SkillFadeInOut)clip;
                                        FadeInOutSkillData data = new FadeInOutSkillData();
                                        data.OnInit(skillFadeInOut);
                                        dataList.Add(data);
                                    }
                                }
                                break;
                            }
                    }
                }
            }

            SkillData resultData = new SkillData(dataList);

            resultData.Length = Data.length;

            Debug.LogError(resultData.Length);

            return resultData;
        }

    }

}

