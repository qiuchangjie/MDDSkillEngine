using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using System.Reflection;

namespace MDDSkillEngine
{
    public sealed class BuffFactory
    {
        private static Assembly skillAssembly;
        public static Assembly SkillAssembly
        {
            get
            {
                if (skillAssembly == null)
                {
                    skillAssembly = Assembly.Load("MDDSkillEngine");

                    return skillAssembly;
                }
                else
                    return skillAssembly;
            }
        }


        public static BuffBase AcquireBuff(string bufName)
        {
            object buf = SkillAssembly.CreateInstance(SkillAssembly.GetName().Name + "." + bufName);

            entiity ent = GameObject.Find("GameObject").GetComponent<entiity>();

            ((BuffBase)buf).OnInit(null);

            return (BuffBase)buf;
        }
    }
}


