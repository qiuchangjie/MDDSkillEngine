using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using System;

namespace MDDSkillEngine
{
    public class BuffFactoryHelper : IBuffFactory
    {
        public BuffBase AcquireBuff(string bufName, object Target, object From,object userData = null)
        {
            Type buffType = Utility.Assembly.GetType(Utility.Text.Format("MDDSkillEngine.{0}", bufName));

            BuffBase buff = (BuffBase)ReferencePool.Acquire(buffType);

            buff.OnInit(null, Target, From,userData: userData);

            return buff;
        }
    }

   

}

