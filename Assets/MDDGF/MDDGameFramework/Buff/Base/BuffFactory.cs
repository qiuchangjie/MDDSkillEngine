using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace MDDGameFramework
{
    public class BuffFactory
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


        public static BuffBase AcquireBuff(string bufName, int bufID)
        {
            Type type = SkillAssembly.GetType(SkillAssembly.GetName().Name + "." + bufName);

            IReference i = ReferencePool.Acquire(type);

            //((BuffBase)buf).OnInit(null,null,testm);

            return (BuffBase)i;
        }
    }
}
