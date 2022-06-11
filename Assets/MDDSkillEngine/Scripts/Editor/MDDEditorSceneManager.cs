using MDDSkillEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MDDSkillEngine
{
    public static class MDDEditorSceneManager
    {
        [MenuItem("Scene/EditorSkillTimeline")]
        private static void OpenEditorSkillTimelineScene()
        {
            EditorSceneManager.sceneOpened += CallBackSceneOpen;
            EditorSceneManager.OpenScene(AssetUtility.GetSceneAsset("EditorSkillTimeline"), OpenSceneMode.Single);            
        }

        [MenuItem("Scene/Launch")]
        private static void OpenLaunchScene()
        {
            EditorSceneManager.OpenScene(AssetUtility.GetSceneAsset("MDDSkillLauncher"), OpenSceneMode.Single);
        }

        private static void CallBackSceneOpen(Scene scene, OpenSceneMode openSceneMode)
        {
            if (scene.name == "EditorSkillTimeline")
            {
                MDDSkillTimelineEditor.Instance.ClearRoot();
            }
        }


    }
}



