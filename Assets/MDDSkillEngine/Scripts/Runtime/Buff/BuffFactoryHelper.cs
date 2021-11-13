using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using System;

namespace MDDSkillEngine
{
    public class BuffFactoryHelper : IBuffFactory
    {
        public BuffBase AcquireBuff(string bufName, object Target, object From)
        {
            Type buffType = Utility.Assembly.GetType(Utility.Text.Format("MDDSkillEngine.{0}", bufName));

            //ReferencePool.Acquire(buffType);

            //entiity ent = GameObject.Find("GameObject").GetComponent<entiity>();

            BuffBase buff = (BuffBase)ReferencePool.Acquire(buffType);

            buff.OnInit(null, Target, From);

            return buff;
        }
    }

}

