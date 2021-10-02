using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDGameFramework
{
    internal abstract class BuffSystemBase 
    {
        internal abstract void OnUpdate(float elapseSeconds, float realElapseSeconds);

        internal abstract void AddBuff(string buffName,object target,object from);

        internal abstract void Shutdown();
    }
}


