﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MDDGameFramework
{
    public interface IBuffSystem
    {
        //void AddBuff(int bufID,object target,object from);

        float PlayableSpeed
        {
            get;
            set;
        }

        object Owner
        {
            get;
        }

        void RemoveBuff(int bufID);
        void RemoveBuff(BuffBase buf);
        void RemoveBuff(object from);
        void RemoveBuff(Type type);
        void RemoveBuff(object from,Type type);
        void RemoveAllBuff();

        bool HasBuff(int bufID);

        bool HasBuff(Type type);

        bool HasBuff(string name);

        BuffBase GetBuff(string name);

        void ClearAllBuff();


    }
}


