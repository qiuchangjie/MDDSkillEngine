#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using MDDSkillEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.IO;
using MDDGameFramework;
using System.Collections.Generic;

namespace Slate.ActionClips
{
    [Description("生成特效")]
    [Attachable(typeof(EffectTrack))]
    public class EffectInstance : ActorActionClip
    {

        [SerializeField]
        [HideInInspector]
        private float _length = 1f;

        public override float length
        {
            get { return useSpeed && path != null ? path.length / Mathf.Max(speed, 0.01f) : _length; }
            set { _length = value; }
        }

        [ValueDropdown("GetEffects")]
        public string EffectName;

        public ParticleSystem pat;

        [OnValueChanged("OnTimeScale")]
        public float PlayableSpeed = 1f;

        [OnValueChanged("OnSetPosition")]
        public Vector3 localeftPostion;

        [OnValueChanged("OnSetRotation")]
        public Quaternion localRotation;

        [OnValueChanged("OnSetScale")]
        public Vector3 localScale = Vector3.one;

        public Path path;

        [InfoBox("曲线运动是否使用自定义速度 \n" +
           "使用了自定义速度将激发魔法弹效应，timeline只负责生成物体，之后的行为由物体本身驱动")]
        public bool useSpeed;
        public float speed = 3f;

        [Button("创建位移路径")]
        public void CreatePath()
        {
            if (path == null)
            {
                path = BezierPath.Create(((Cutscene)root).transform);
            }
        }

        [Button("切换到对应的path")]
        public void ChangeSelect()
        {
            Selection.activeGameObject = path.gameObject;
        }


     

        //public override string info
        //{
        //    get { return string.Format("{0} Actor", activeState); }
        //}

        protected override void OnEnter()
        {

            if (pat == null)
            {
                Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath(AssetUtility.GetEntityAsset(EffectName, EntityType.Effect), typeof(Object));
                GameObject obj = Instantiate(asset,((Cutscene)root).transform.parent) as GameObject;
                pat = obj.GetComponent<ParticleSystem>();
                pat.useAutoRandomSeed = false;
                obj.transform.localPosition = localeftPostion;
                obj.transform.rotation = localRotation;
                obj.transform.localScale = localScale;
                var main = pat.main;
                main.simulationSpeed = PlayableSpeed;

            }

        }

        protected override void OnUpdate(float time, float previousTime)
        {
            base.OnUpdate(time, previousTime);

            //time *= PlayableSpeed;

        
            if (pat != null)
            {
                if (path != null)
                {
                    var newPos = path.GetPointAt(time / length);
                    pat.transform.position = newPos;
                }
          
                pat.Simulate((time - previousTime) * PlayableSpeed, true, false);


            }


            SceneView.RepaintAll();
        }

        protected override void OnExit()
        {
            localeftPostion = pat.gameObject.transform.localPosition;
            localRotation = pat.gameObject.transform.localRotation;
            localScale = pat.gameObject.transform.localScale;
            PlayableSpeed = pat.playbackSpeed;
            DestroyImmediate(pat.gameObject);
        }

        protected override void OnReverseEnter()
        {

        }

        protected override void OnReverse()
        {
            //Debug.LogError("OnReverse");
        }

        private void OnSetPosition()
        {
            Debug.LogError("change");
            if (pat != null)
                pat.gameObject.transform.localPosition = localeftPostion;
        }

        private void OnSetRotation()
        {
            Debug.LogError("change");
            if (pat != null)
                pat.gameObject.transform.localRotation = localRotation;
        }

        private void OnSetScale()
        {
            Debug.LogError("change");
            if (pat != null)
                pat.gameObject.transform.localScale = localScale;
        }

        private void OnTimeScale()
        {
            Debug.LogError("change");
            if (pat != null)
            {
                var main = pat.main;
                main.simulationSpeed = PlayableSpeed;

            }

        }


#if UNITY_EDITOR
        private IEnumerable<string> GetEffects()
        {
            if (NPBlackBoardEditorInstance.Effects.Count == 0)
            {
                string fullPath = Application.dataPath + "/MDDSkillEngine/Prefabs/Effect";
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
                            NPBlackBoardEditorInstance.Effects.Add(files[i].Name.Remove(files[i].Name.LastIndexOf(".")));
                        }

                    }
                }
            }

            return NPBlackBoardEditorInstance.Effects;
        }

#endif
    }
}
#endif