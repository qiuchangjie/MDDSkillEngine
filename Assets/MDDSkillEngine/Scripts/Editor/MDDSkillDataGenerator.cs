using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MDDSkillDataGenerator : EditorWindow
{
    [MenuItem("MDDSkillEngine/GeneratorSkillData")]
    public static void GeneratorSkillData()
    {
        //路径
        string fullPath = Application.dataPath + "/MDDSkillEngine/SkillData/";
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
                    string path = "Assets/MDDSkillEngine/SkillData/" + files[i].Name;
                    Debug.Log("预制体路径" + path);
                    GameObject obj = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject)) as GameObject;
                    Debug.Log("obj的名字" + obj.name);
                    //Transform tra = obj.transform.FindChild("Label");
                    //UILabel[] labels = obj.transform.GetComponentsInChildren<UILabel>(true);
                    //foreach (UILabel lab in labels)
                    //{
                    //    lab.color = Color.red;
                    //}


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
}
