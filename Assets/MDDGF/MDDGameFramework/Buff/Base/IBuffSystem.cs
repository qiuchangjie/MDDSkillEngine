using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public interface IBuffSystem
    {
        void AddBuff(int bufID,object target,object from);

        void RemoveBuff();
    }
}


