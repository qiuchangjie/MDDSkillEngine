using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public interface IBuffSystemManager
    {
        void AddBuff(string buffName,object target, object from);

        IBuffSystem CreatBuffSystem();
    }
}
