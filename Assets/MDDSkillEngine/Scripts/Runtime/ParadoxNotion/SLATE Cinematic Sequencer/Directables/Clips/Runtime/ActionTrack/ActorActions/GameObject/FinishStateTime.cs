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
    [Description("结束状态时间点")]
    [Attachable(typeof(FinishStateTrack))]
    public class FinishStateTime : ActorActionClip
    {

        [SerializeField]
        [HideInInspector]
        private float _length = 0;

    }
}