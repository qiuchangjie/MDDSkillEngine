﻿#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using MDDSkillEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using MDDGameFramework;
using System;
using System.IO;

namespace Slate.ActionClips
{
    [Description("生成碰撞体")]
    [Attachable(typeof(ColliderTrack))]
    public class InstanceCollider : ActorActionClip
    {

        [SerializeField]
        [HideInInspector]
        private float _length = 1f;

        public override float length
        {
            get { return useSpeed && path != null ? path.length / Mathf.Max(speed, 0.01f) : _length; }
            set { _length = value; }
        }

        [BoxGroup("碰撞体设置")]
        [ValueDropdown("GetColliders")]
        public string ColliderName;

        [BoxGroup("碰撞体设置")]
        [ValueDropdown("GetColliderLogic")]
        public string ColliderLogic;

        [BoxGroup("碰撞体设置")]
        public float Speed;

        [BoxGroup("碰撞体设置")]
        [ValueDropdown("GetBuffs")]
        public string AddBuffName;

        [BoxGroup("hitbuff参数")]
        [ValueDropdown("GetEffect")]
        public string EffectName;


        public BoxCollider col;

        public SphereCollider m_SphereCollider;

        public CapsuleCollider m_CapsuleCollider;

        public Collider m_Collider;

        [OnValueChanged("OnSetPosition")]
        public Vector3 localeftPostion;

        [OnValueChanged("OnSetRotation")]
        public Quaternion localRotation;

        [OnValueChanged("OnSetScale")]
        public Vector3 localScale = Vector3.one;

        [OnValueChanged("OnSetColSize")]
        public Vector3 boundSize = Vector3.one;

        [OnValueChanged("OnSetColCenter")]
        public Vector3 boundCenter;

        public float radius = 1f;

        public float height = 1f;

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

        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
        }

        protected override void OnEnter()
        {

            if (m_Collider == null)
            {
                UnityEngine.Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath(AssetUtility.GetEntityAsset(ColliderName, EntityType.Collider), typeof(UnityEngine.Object));
                GameObject obj = Instantiate(asset,((Cutscene)root).transform.parent) as GameObject;
                m_Collider = obj.GetComponent<Collider>();               
                obj.transform.localPosition = localeftPostion;
                obj.transform.rotation = localRotation;
                obj.transform.localScale = localScale;

                if (m_Collider is BoxCollider)
                {
                    col = (BoxCollider)m_Collider;
                    col.size = boundSize;
                    col.center = boundCenter;
                }

                if (m_Collider is SphereCollider)
                {
                    m_SphereCollider = (SphereCollider)m_Collider;
                    m_SphereCollider.radius = radius;
                    m_SphereCollider.center = boundCenter;
                }

                if (m_Collider is CapsuleCollider)
                {
                    m_CapsuleCollider = (CapsuleCollider)m_Collider;
                    m_CapsuleCollider.radius = radius;
                    m_CapsuleCollider.height = height;
                    m_CapsuleCollider.center = boundCenter;
                }
            }

        }

        protected override void OnUpdate(float time, float previousTime)
        {
            base.OnUpdate(time, previousTime);

            if (m_Collider != null)
            {
                if (path != null)
                {
                    var newPos = path.GetPointAt(time / length);
                    m_Collider.transform.position = newPos;
                }
              
            }


            SceneView.RepaintAll();
        }

        protected override void OnExit()
        {
            localeftPostion = m_Collider.gameObject.transform.localPosition;
            localRotation = m_Collider.gameObject.transform.localRotation;
            localScale = m_Collider.gameObject.transform.localScale;


            if (m_Collider is BoxCollider)
            {
                col = (BoxCollider)m_Collider;
                boundCenter = col.center;
                boundSize = col.size;
            }

            if (m_Collider is SphereCollider)
            {
                m_SphereCollider = (SphereCollider)m_Collider;
                boundCenter = m_SphereCollider.center;
                radius = m_SphereCollider.radius;
            }

            if (m_Collider is CapsuleCollider)
            {
                m_CapsuleCollider = (CapsuleCollider)m_Collider;
                radius = m_CapsuleCollider.radius;
                height = m_CapsuleCollider.height;
                boundCenter = m_CapsuleCollider.center;
            }

            DestroyImmediate(m_Collider.gameObject);
        }

        protected override void OnReverseEnter()
        {

        }

        protected override void OnReverse()
        {
            
        }




#if UNITY_EDITOR
        private void OnSetPosition()
        {
            Debug.LogError("change");
            if (col != null)
                col.gameObject.transform.localPosition = localeftPostion;
        }

        private void OnSetRotation()
        {
            Debug.LogError("change");
            if (col != null)
                col.gameObject.transform.localRotation = localRotation;
        }

        private void OnSetScale()
        {
            Debug.LogError("change");
            if (col != null)
                col.gameObject.transform.localScale = localScale;
        }

        private void OnSetColSize()
        {
            Debug.LogError("change");
            if (col != null)
            {
                col.size = boundSize;
            }
        }
        private void OnSetColCenter()
        {
            Debug.LogError("change");
            if (col != null)
            {
                col.center = boundCenter;
            }

        }

        private IEnumerable<string> GetBuffs()
        {
            if (NPBlackBoardEditorInstance.buffs.Count == 0)
            {
                //通过反射获取所有buff的名字
                List<Type> types = new List<Type>();
                Utility.Assembly.GetTypesByFather(types, typeof(BuffBase));
                List<string> buffsName = new List<string>();
                foreach (var type in types)
                {
                    buffsName.Add(type.Name);
                }
                NPBlackBoardEditorInstance.buffs = buffsName;
            }

            return NPBlackBoardEditorInstance.buffs; ;
        }

        private IEnumerable<string> GetColliderLogic()
        {
            if (NPBlackBoardEditorInstance.ColliderLogic.Count == 0)
            {
                //通过反射获取所有ColliderLogic的名字
                List<Type> types = new List<Type>();
                Utility.Assembly.GetTypesByFather(types, typeof(ColliderBase));
                List<string> colliderName = new List<string>();
                foreach (var type in types)
                {
                    colliderName.Add(type.Name);
                }
                NPBlackBoardEditorInstance.ColliderLogic = colliderName;
            }

            return NPBlackBoardEditorInstance.ColliderLogic; 
        }

   

        private IEnumerable<string> GetColliders()
        {
            if (NPBlackBoardEditorInstance.Collider.Count == 0)
            {
                string fullPath = Application.dataPath + "/MDDSkillEngine/Prefabs/Collider";
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
                            NPBlackBoardEditorInstance.Collider.Add(files[i].Name.Remove(files[i].Name.LastIndexOf(".")));
                        }
                            
                    }
                }                                      
            }

            return NPBlackBoardEditorInstance.Collider; 
        }

        private IEnumerable<string> GetEffect()
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

            return NPBlackBoardEditorInstance.Effects; ;
        }

     
        
#endif
    }
}
#endif