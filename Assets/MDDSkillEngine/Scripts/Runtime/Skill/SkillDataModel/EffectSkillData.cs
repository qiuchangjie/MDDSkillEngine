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
        }
    }
}


