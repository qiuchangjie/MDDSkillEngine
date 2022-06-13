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

        public static IEnumerator WaitForFrame()
        {
            yield return null;
        }
    }
}
