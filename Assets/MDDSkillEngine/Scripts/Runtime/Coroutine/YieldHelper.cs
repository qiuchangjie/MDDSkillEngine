using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MDDSkillEngine
{
    public static class YieldHelper
    {
        public static WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
        
        public static WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();

        public static IEnumerator WaitForSeconds(float waittime = 0.1f,bool ignoreTimeScale = false)
        {
            float duration = 0;
            while (duration <= waittime)
            {
                duration += (ignoreTimeScale ? Time.unscaledDeltaTime : Time.timeScale);
                yield return null;
            }
        }
    }
}
