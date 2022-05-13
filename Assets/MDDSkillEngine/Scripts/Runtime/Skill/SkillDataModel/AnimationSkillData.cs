using MDDGameFramework.Runtime;
using Slate;
using Slate.ActionClips;
using System;

namespace MDDSkillEngine
{
    [Serializable]
    public class AnimationSkillData : SkillDataBase
    {
        public string AnimationName= "";

        public override void OnInit(ActionClip data)
        {
            base.OnInit(data);
            PlayAnimatorClip Clip = data as PlayAnimatorClip;

            if (Clip == null)
            {
                Log.Error("{0}数据转换失败", LogConst.SKillTimeline);
            }

            DataType = SkillDataType.Animation;

            AnimationName = Clip.animationName;
        }
    }
}


