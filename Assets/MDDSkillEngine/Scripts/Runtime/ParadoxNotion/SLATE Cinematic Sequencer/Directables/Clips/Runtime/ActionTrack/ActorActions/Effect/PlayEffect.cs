using UnityEngine;
using System.Collections;
using MDDSkillEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace Slate.ActionClips
{
    [Category("GameObject")]
    [Description("生成特效")]
    public class PlayEffect : ActorActionClip<ParticleSystem>
    {

        [SerializeField]
        [HideInInspector]
        private float _length = 1f;

        public string EffectName;

        public ParticleSystem pat;

        [OnValueChanged("OnTimeScale")]
        public float PlayableSpeed = 0.5f;

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
                var main = pat.main;
                main.simulationSpeed = PlayableSpeed;

            }

        }

        protected override void OnUpdate(float time, float previousTime)
        {
            base.OnUpdate(time, previousTime);

            //time *= PlayableSpeed;

            if (pat != null)
                pat.Simulate((time - previousTime) * PlayableSpeed, true, false);

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
    }
}