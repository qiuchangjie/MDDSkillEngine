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
    [Description("判定技能是否进入CD的时间点")]
    [Attachable(typeof(CDTrack))]
    public class CD : ActorActionClip
    {

        [SerializeField]
        [HideInInspector]
        private float _length = 0;

    }
}