﻿using MDDGameFramework.Runtime;
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

        public override void OnInit(ActionClip data)
        {
            base.OnInit(data);
            InstanceCollider instanceCollider = data as InstanceCollider;

            if (instanceCollider == null)
            {
                Log.Error("{0}数据转换失败",LogConst.SKillTimeline);
            }

            DataType = SkillDataType.Collider;

            localeftPostion = instanceCollider.localeftPostion;
            localRotation = instanceCollider.localRotation;
            localScale = instanceCollider.localScale;
            boundSize = instanceCollider.boundSize;                
            boundCenter = instanceCollider.boundCenter;
            ResouceName = instanceCollider.ColliderName;
        }
    }
}

