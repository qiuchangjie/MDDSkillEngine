using MDDGameFramework.Runtime;
using Slate;
using Slate.ActionClips;
using System;
using UnityEngine;


namespace MDDSkillEngine
{
    [Serializable]
    public class ColliderSkillData : SkillDataBase
    {
        public Vector3 localeftPostion;

        public Quaternion localRotation;

        public Vector3 localScale;

        public Vector3 boundSize;

        public Vector3 boundCenter;

        public float redius;

        public float height;

        public string ColliderName;

        public string ColliderLogic;

        public float Speed;

        public string AddBuffName;

        public string EffectName;


        public bool hasPath;
        public bool useSpeed;
        public Vector3[] bezierPath;
        public Vector3 bezierPathParentPosition;
        public Quaternion bezierPathParentRotation;
        public float bezierPathLength;

#if UNITY_EDITOR
        public override void OnInit(ActionClip data)
        {
            base.OnInit(data);
            InstanceCollider instanceCollider = data as InstanceCollider;

            if (instanceCollider == null)
            {
                Log.Error("{0}数据转换失败",LogConst.SKillTimeline);
            }

            DataType = SkillDataType.Collider;

            ResouceName = instanceCollider.ColliderName;
            localeftPostion = instanceCollider.localeftPostion;
            localRotation = instanceCollider.localRotation;
            localScale = instanceCollider.localScale;
            boundSize = instanceCollider.boundSize;                
            boundCenter = instanceCollider.boundCenter;
            redius = instanceCollider.radius;
            height = instanceCollider.height;   
            ColliderName = instanceCollider.ColliderName;
            ColliderLogic = instanceCollider.ColliderLogic;
            Speed = instanceCollider.Speed;
            AddBuffName = instanceCollider.AddBuffName;
            EffectName = instanceCollider.EffectName;

            if (instanceCollider.path != null)
            {
                BezierPath path = instanceCollider.path as BezierPath;
                hasPath = true;
                useSpeed = instanceCollider.useSpeed; 
                bezierPath = path._sampledPathPoints;
                bezierPathParentPosition = path.transform.localPosition;
                bezierPathParentRotation = path.transform.localRotation;
                bezierPathLength = path.length;
            }
        }
#endif
    }
}

