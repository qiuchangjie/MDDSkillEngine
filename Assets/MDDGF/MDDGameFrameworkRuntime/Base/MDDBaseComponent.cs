using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework.Runtime
{
    public sealed class MDDBaseComponent : MDDGameFrameworkComponent
    {
               
        private void Update()
        {
            MDDGameFrameworkEntry.Update(Time.deltaTime,Time.unscaledDeltaTime);
        }
    }
}

