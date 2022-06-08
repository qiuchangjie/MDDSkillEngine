using MDDGameFramework.Runtime;
using Slate;
using Slate.ActionClips;
using System;
using UnityEngine;

namespace MDDSkillEngine
{
    [Serializable]
    public class EffectSkillData : SkillDataBase
    {
        public Vector3 localeftPostion;

        public Quaternion localRotation;

        public Vector3 localScale;


        public bool hasPath;
        public bool useSpeed;
        public Vector3[] bezierPath;
        public Vector3 bezierPathParentPosition;
        public Quaternion bezierPathParentRotation;
        public float bezierPathLength;

        public override void OnInit(ActionClip data)
        {
  
            base.OnInit(data);
            EffectInstance clip = data as EffectInstance;
            if (clip == null)
            {
                Log.Error("{0}数据转换失败", LogConst.SKillTimeline);
            }
            DataType = SkillDataType.Effect;

            ResouceName = clip.EffectName;

            localeftPostion = clip.localeftPostion;
            localRotation = clip.localRotation;
            localScale = clip.localScale;

            if (clip.path != null)
            {
                BezierPath path = clip.path as BezierPath;
                hasPath = true;
                useSpeed = clip.useSpeed; 
                bezierPath = path._sampledPathPoints;
                bezierPathParentPosition = path.transform.localPosition;
                bezierPathParentRotation = path.transform.localRotation;
                bezierPathLength = path.length;
            }
        }
    }
}


