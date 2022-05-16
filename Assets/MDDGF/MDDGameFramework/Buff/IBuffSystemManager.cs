﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public interface IBuffSystemManager
    {
        void AddBuff(string buffSystemName,string buffName,object target, object from);

        IBuffSystem CreatBuffSystem(string name, object owner);

        IBuffSystem GetBuffSystem(string name);
    }
}
