using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public sealed class MDDBaseComponent : MDDGameFrameworkComponent
    {
               
        private void Update()
        {
            MDDGameFrameworkEntry.Update(Time.deltaTime,Time.unscaledDeltaTime);
        }
    }
}

