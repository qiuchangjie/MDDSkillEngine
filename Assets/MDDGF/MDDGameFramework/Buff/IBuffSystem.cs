using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public interface IBuffSystem
    {
        //void AddBuff(int bufID,object target,object from);

        object Owner
        {
            get;
        }

        void RemoveBuff(int bufID);

        bool HasBuff(int bufID);

        void ClearBuff();


    }
}


