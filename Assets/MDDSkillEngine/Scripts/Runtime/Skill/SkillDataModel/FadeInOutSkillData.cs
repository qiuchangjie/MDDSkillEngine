using MDDGameFramework.Runtime;
using Slate;
using Slate.ActionClips;
using System;
using UnityEngine;


namespace MDDSkillEngine
{
    public class FadeInOutSkillData : SkillDataBase
    {
        public FadeInOutType type;

# if UNITY_EDITOR
        public override void OnInit(ActionClip data)
        {
            base.OnInit(data);
            SkillFadeInOut clip = data as SkillFadeInOut;
            if (clip == null)
            {
                Log.Error("{0}数据转换失败", LogConst.SKillTimeline);
            }

            DataType = SkillDataType.InOut;
            type = clip.Type;
        }
#endif
    }

    public enum FadeInOutType
    {
        In,
        Out,
    }
}

