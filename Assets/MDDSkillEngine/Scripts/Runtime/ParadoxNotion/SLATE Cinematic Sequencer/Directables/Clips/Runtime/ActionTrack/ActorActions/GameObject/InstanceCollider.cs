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

        [BoxGroup("碰撞体设置")]
        [ValueDropdown("GetColliders")]
        [OnValueChanged("OnColliderChange")]
        public string ColliderName;

        [BoxGroup("碰撞体设置")]
        [ShowIf("_enabled", 1)]
        public float Speed;

        [BoxGroup("碰撞体设置")]
        [ShowIf("_enabled", 1)]
        [ValueDropdown("GetBuffs")]
        [OnValueChanged("OnBuffChange")]
        public string AddBuffName;

        [BoxGroup("碰撞体设置")]
        [ShowIf("_enabled1", 1)]
        public int EffectID;

        [BoxGroup("碰撞体设置")]
        [ShowIf("_enabled1", 1)]
        public float force;

        [BoxGroup("碰撞体设置")]
        [ShowIf("_enabled1", 1)]
        public float duration;

        public BoxCollider col;

        [OnValueChanged("OnSetPosition")]
        public Vector3 localeftPostion;

        [OnValueChanged("OnSetRotation")]
        public Quaternion localRotation;

        [OnValueChanged("OnSetScale")]
        public Vector3 localScale;

        [OnValueChanged("OnSetColSize")]
        public Vector3 boundSize;

        [OnValueChanged("OnSetColCenter")]
        public Vector3 boundCenter;

        
        private bool _enabled = false;

        private bool _enabled1 = false;


        public override float length
        {
            get { return _length; }
            set { _length = value; }
        }

       

        //public override string info
        //{
        //    get { return string.Format("{0} Actor", activeState); }
        //}

        protected override void OnEnter()
        {

            if (col == null)
            {
                UnityEngine.Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath(AssetUtility.GetEntityAsset(ColliderName, EntityType.Collider), typeof(UnityEngine.Object));
                GameObject obj = Instantiate(asset) as GameObject;
                col = obj.GetComponent<BoxCollider>();               
                obj.transform.localPosition = localeftPostion;
                obj.transform.rotation = localRotation;
                obj.transform.localScale = localScale;

                col.size = boundSize;
                col.center = boundCenter;
              

            }

        }

        protected override void OnUpdate(float time, float previousTime)
        {
            base.OnUpdate(time, previousTime);



            SceneView.RepaintAll();
        }

        protected override void OnExit()
        {
            localeftPostion = col.gameObject.transform.localPosition;
            localRotation = col.gameObject.transform.localRotation;
            localScale = col.gameObject.transform.localScale;
            boundCenter = col.center;
            boundSize = col.size;
            DestroyImmediate(col.gameObject);
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


#if UNITY_EDITOR
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

        private void OnBuffChange()
        {
            if (AddBuffName == "PushHit")
            {
                _enabled1 = true;
            }
            else
            {
                _enabled1 = false;
            }
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

            return NPBlackBoardEditorInstance.Collider; ;
        }

        private IEnumerable<string> GetEffect()
        {
            if (NPBlackBoardEditorInstance.Collider.Count == 0)
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
                            NPBlackBoardEditorInstance.Collider.Add(files[i].Name.Remove(files[i].Name.LastIndexOf(".")));
                        }

                    }
                }
            }

            return NPBlackBoardEditorInstance.Collider; ;
        }

        private void OnColliderChange()
        {
            if (ColliderName == "NormalMoveCollider")
            {
                _enabled = true;
            }
            else
            {
                _enabled = false;
            }
        }

        
#endif
    }
}