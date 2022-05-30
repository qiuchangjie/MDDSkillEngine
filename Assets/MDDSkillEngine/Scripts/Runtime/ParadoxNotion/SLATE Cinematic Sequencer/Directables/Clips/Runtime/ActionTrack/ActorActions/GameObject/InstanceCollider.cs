using UnityEngine;
using System.Collections;
using MDDSkillEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace Slate.ActionClips
{
    [Description("生成碰撞体")]
    [Attachable(typeof(ColliderTrack))]
    public class InstanceCollider : ActorActionClip
    {

        [SerializeField]
        [HideInInspector]
        private float _length = 1f;

        public string ColliderName;

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
                Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath(AssetUtility.GetEntityAsset(ColliderName, EntityType.Collider), typeof(Object));
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
    }
}