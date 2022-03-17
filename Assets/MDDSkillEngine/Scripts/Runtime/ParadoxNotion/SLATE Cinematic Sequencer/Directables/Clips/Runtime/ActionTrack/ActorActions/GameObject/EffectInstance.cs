using UnityEngine;
using System.Collections;
using MDDSkillEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace Slate.ActionClips
{
    [Category("GameObject")]
    [Description("生成特效")]
    public class EffectInstance : ActorActionClip
    {

        [SerializeField]
        [HideInInspector]
        private float _length = 1f;

        public string EffectName;

        public ParticleSystem pat;

        [OnValueChanged("OnSetPosition")]
        public Vector3 localeftPostion;

        [OnValueChanged("OnSetRotation")]
        public Quaternion localRotation;

        [OnValueChanged("OnSetScale")]
        public Vector3 localScale;

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

            if (pat == null)
            {
                Object asset = UnityEditor.AssetDatabase.LoadAssetAtPath(AssetUtility.GetEntityAsset(EffectName), typeof(Object));
                GameObject obj = Instantiate(asset) as GameObject;
                pat = obj.GetComponent<ParticleSystem>();
                obj.transform.localPosition = localeftPostion;
                obj.transform.rotation = localRotation;
                obj.transform.localScale = localScale;
            }
            
        }

        protected override void OnUpdate(float time, float previousTime)
        {
            base.OnUpdate(time, previousTime);
          
            if (pat != null)
                pat.Simulate(time - previousTime, true,false);

            SceneView.RepaintAll();
        }

        protected override void OnExit()
        {
            localeftPostion = pat.gameObject.transform.localPosition;
            localRotation = pat.gameObject.transform.localRotation;
            localScale = pat.gameObject.transform.localScale;
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
            if (pat != null)
                pat.gameObject.transform.localPosition = localeftPostion;
        }

        private void OnSetRotation()
        {
            if (pat != null)
                pat.gameObject.transform.localRotation = localRotation;
        }

        private void OnSetScale()
        {
            if (pat != null)
                pat.gameObject.transform.localScale = localScale;
        }
    }
}