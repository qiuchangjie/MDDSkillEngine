#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using MDDSkillEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.IO;
using MDDGameFramework;
using System.Collections.Generic;
using System;

namespace Slate.ActionClips
{
    [Description("生成实体")]
    [Attachable(typeof(EntityTrack))]
    public class EntityInstance : ActorActionClip
    {

        [SerializeField]
        [HideInInspector]
        private float _length = 1f;

        [ValueDropdown("GetHero")]
        public string EntityName;

        [ValueDropdown("GetHeroLogic")]
        public string EntityLogic;

        public GameObject point;

        public float PlayableSpeed = 1f;

        [OnValueChanged("OnSetPosition")]
        public Vector3 localeftPostion;

        [OnValueChanged("OnSetRotation")]
        public Quaternion localRotation;

        [OnValueChanged("OnSetScale")]
        public Vector3 localScale = Vector3.one;

        public override float length
        {
            get { return _length; }
            set { _length = value; }
        }


        protected override void OnEnter()
        {

            if (point == null)
            {
                point = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                point.transform.SetParent(((Cutscene)root).transform.parent);
                point.transform.localPosition = localeftPostion;
                point.transform.rotation = localRotation;
                point.transform.localScale = localScale;
            }

        }

        protected override void OnUpdate(float time, float previousTime)
        {
            base.OnUpdate(time, previousTime);

            //time *= PlayableSpeed;         
            SceneView.RepaintAll();
        }

        protected override void OnExit()
        {
            localeftPostion = point.gameObject.transform.localPosition;
            localRotation = point.gameObject.transform.localRotation;
            localScale = point.gameObject.transform.localScale;
            DestroyImmediate(point.gameObject);
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
            if (point != null)
                point.gameObject.transform.localPosition = localeftPostion;
        }

        private void OnSetRotation()
        {
            if (point != null)
                point.gameObject.transform.localRotation = localRotation;
        }

        private void OnSetScale()
        {
            if (point != null)
                point.gameObject.transform.localScale = localScale;
        }




#if UNITY_EDITOR
        private IEnumerable<string> GetHero()
        {
            if (NPBlackBoardEditorInstance.Hero.Count == 0)
            {
                string fullPath = Application.dataPath + "/MDDSkillEngine/Prefabs/Hero";
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
                            NPBlackBoardEditorInstance.Hero.Add(files[i].Name.Remove(files[i].Name.LastIndexOf(".")));
                        }

                    }
                }
            }

            return NPBlackBoardEditorInstance.Hero;
        }


        private IEnumerable<string> GetHeroLogic()
        {
            if (NPBlackBoardEditorInstance.HeroLogic.Count == 0)
            {
                //通过反射获取所有ColliderLogic的名字
                List<Type> types = new List<Type>();
                Utility.Assembly.GetTypesByFather(types, typeof(TargetableObject));
                List<string> colliderName = new List<string>();
                foreach (var type in types)
                {
                    colliderName.Add(type.Name);
                }
                NPBlackBoardEditorInstance.HeroLogic = colliderName;
            }

            return NPBlackBoardEditorInstance.HeroLogic;
        }

#endif
    }
}
#endif