using Animancer;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class AnimationContainer : SerializedMonoBehaviour
    {
        [SerializeField]
        [DictionaryDrawerSettings(KeyLabel = "动画名", ValueLabel = "AnimationClip")]
        [ShowInInspector]
        [InfoBox("动画资源容器字典")]
        private Dictionary<string, ClipState.Transition> animDic = new Dictionary<string, ClipState.Transition>();

        public ClipState.Transition GetAnimation(string animName)
        {
            ClipState.Transition anim;
            if (animDic.TryGetValue(animName, out anim))
            {
                return anim;
            }

            return null;
        }
    }
}


