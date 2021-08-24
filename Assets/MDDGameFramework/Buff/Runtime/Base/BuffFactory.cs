using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    public class BuffFactory
    {
        public static BuffBase AcquireBuff()
        {

            Buff buf = new Buff();


            return buf;
        }
    }
}
