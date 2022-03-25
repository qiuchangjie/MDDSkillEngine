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
            string fullPath = Application.dataPath + "/MDDSkillEngine/SkillPrefab/";
            string savePath = Application.dataPath + "/MDDSkillEngine/SkillData/";
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
                        string path = "Assets/MDDSkillEngine/SkillPrefab/" + files[i].Name;
                        Debug.Log("预制体路径" + path);
                        GameObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
                        Debug.Log("obj的名字" + obj.name);
                        //Transform tra = obj.transform.FindChild("Label");
                        //UILabel[] labels = obj.transform.GetComponentsInChildren<UILabel>(true);
                        //foreach (UILabel lab in labels)
                        //{
                        //    lab.color = Color.red;
                        //}

                        Cutscene Data = obj.GetComponent<Cutscene>();
                       // byte[] bytes;

                        //if (Data != null)
                        //{

                        //    //string json = CatJson.JsonParser.ToJson<SkillData>(HandleSkillData(Data));
                        //    byte[] bytes = SerializationUtility.SerializeValue(HandleSkillData(Data), DataFormat.Binary);
                        //    using (StreamWriter sr = new StreamWriter(savePath + "\\name.bytes"))
                        //    {
                        //        sr.Write(bytes);
                        //    }
                        //}

                        //string jsontest = JsonUtility.ToJson(HandleSkillData(Data));

                        byte[] bytes = SerializationUtility.SerializeValue(HandleSkillData(Data), DataFormat.Binary);

                        File.WriteAllBytes(savePath + "name.bytes", bytes);

                       // byte[] bytes = SerializationUtility.SerializeValue(HandleSkillData(Data), DataFormat.Binary);

                        byte[] bytes1 = File.ReadAllBytes(savePath + "name.bytes");

                        // testjson = File.ReadAllText(savePath + "name.json");


                        //SkillData testDeS = JsonUtility.FromJson<SkillData>(testjson);

                        SkillData testDeS = SerializationUtility.DeserializeValue<SkillData>(bytes1, DataFormat.Binary);

                        EffectSkillData testconvertdata = (EffectSkillData)testDeS.skillData[0];

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
                            Debug.LogError("检测到未指定类型的技能数据");
                            continue;
                        case SkillDataType.Effect:
                            {
                                foreach (var clip in track.clips)
                                {
                                    if (clip is EffectInstance)
                                    {
                                        EffectInstance effectInstance = (EffectInstance)clip;
                                        EffectSkillData data = new EffectSkillData();
                                        data.DataType = SkillDataType.Effect;
                                        data.ResouceName = effectInstance.EffectName;
                                        data.localeftPostion = effectInstance.localeftPostion;
                                        data.localRotation = effectInstance.localRotation;
                                        data.localScale = effectInstance.localScale;
                                        Debug.LogError(data.DataType);
                                        dataList.Add(data);
                                    }
                                }
                                break;
                            }
                        case SkillDataType.Animation:
                            {
                                break;
                            }
                        case (SkillDataType.Collider):
                            {
                                break;
                            }
                    }
                }
            }

            SkillData resultData = new SkillData(dataList);

            return resultData;
        }

    }

}

