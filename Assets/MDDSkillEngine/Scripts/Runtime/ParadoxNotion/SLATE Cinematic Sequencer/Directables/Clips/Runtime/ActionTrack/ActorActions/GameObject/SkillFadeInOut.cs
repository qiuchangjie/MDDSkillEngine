using UnityEngine;
using System.Collections;
using MDDSkillEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using MDDGameFramework;
using System;

namespace Slate.ActionClips
{
    [Description("判定技能是否进入无法打断的状态")]
    [Attachable(typeof(SkillFadeInOutTrack))]
    public class SkillFadeInOut : ActorActionClip
    {

        [SerializeField]
        [HideInInspector]
        private float _length = 0;
       
        public FadeInOutType Type = FadeInOutType.In;
    }
}