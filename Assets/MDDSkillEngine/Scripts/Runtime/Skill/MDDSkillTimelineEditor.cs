#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using MDDSkillEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using MDDGameFramework;
using System;
using Slate;
using System.IO;

namespace MDDSkillEngine
{
    
    public class MDDSkillTimelineEditor : MonoBehaviour
    {
        private static MDDSkillTimelineEditor instance;

        public static MDDSkillTimelineEditor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = GameObject.FindGameObjectWithTag("233333").GetComponent<MDDSkillTimelineEditor>();
                    return instance;
                }
                else
                {
                    return instance;
                }
            }
        }

        public Transform instanceRoot;

        [SerializeField]
        private Cutscene curCutscene;

        [InfoBox("选择一个skilltimeline资源")]
        [BoxGroup("EditorSkillTimeline")]
        [ValueDropdown("GetSkillTimeline")]
        [OnValueChanged("ChangeSlate")]
        [SerializeField]
        public string Cutscene;

        [BoxGroup("EditorSkillTimeline")]
        [Button("ClearRoot")]
        public void ClearRoot()
        {
            if (instanceRoot.childCount > 0)
            {
                int count = instanceRoot.childCount;
                for (int i = count - 1; i >= 0; i--)
                {
                    DestroyImmediate(instanceRoot.GetChild(i).gameObject);
                }
            }

            Cutscene = "";
        }

      
        [BoxGroup("EditorSkillTimeline")]
        [Button("保存修改")]
        public void SaveCutScene()
        {
            if (curCutscene != null)
            {
                EditorUtility.SetDirty(curCutscene);
                PrefabUtility.ApplyPrefabInstance(curCutscene.gameObject,InteractionMode.AutomatedAction);
            }
        }

        [BoxGroup("EditorSkillTimeline")]
        [Button("切换到对应的Skilltimeline")]
        private void ChangeToSkilltimeline()
        {
            if (curCutscene != null)
            {
                Selection.activeGameObject = curCutscene.gameObject;
            }
        }


        private void ClearRootInterior()
        {
            if (instanceRoot.childCount > 0)
            {
                int count = instanceRoot.childCount;
                for (int i = count - 1; i >= 0; i--)
                {
                    DestroyImmediate(instanceRoot.GetChild(i).gameObject);
                }
            }
        }

        private void ChangeSlate()
        {
            if (Cutscene == "")
                return;

            ClearRootInterior();
            UnityEngine.Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath(AssetUtility.GetSkillTimelinePrefab(Cutscene), typeof(UnityEngine.Object));
            curCutscene = (PrefabUtility.InstantiatePrefab(asset, transform) as GameObject).GetComponent<Cutscene>();
        }


        private IEnumerable<string> GetSkillTimeline()
        {
            if (NPBlackBoardEditorInstance.SkillTimelines.Count == 0)
            {
                string fullPath = Application.dataPath + "/MDDSkillEngine/SkillRes/SkillTimeline";
                //获得指定路径下面的所有资源文件
                if (Directory.Exists(fullPath))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(fullPath);
                    System.IO.FileInfo[] files = dirInfo.GetFiles("*", SearchOption.AllDirectories); //包括子目录
                    Debug.Log(files.Length);

                    for (int i = 0; i < files.Length; i++)
                    {
                        if (files[i].Name.EndsWith(".prefab"))
                        {
                            NPBlackBoardEditorInstance.SkillTimelines.Add(files[i].Name.Remove(files[i].Name.LastIndexOf(".")));
                        }

                    }
                }
            }

            return NPBlackBoardEditorInstance.SkillTimelines;
        }

    }
}
#endif
