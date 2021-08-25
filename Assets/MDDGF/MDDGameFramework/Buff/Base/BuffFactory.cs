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


        public static BuffBase AcquireBuff(string bufName,int bufID)
        {          
            object buf = SkillAssembly.CreateInstance(SkillAssembly.GetName().Name +"."+bufName);           

            //((BuffBase)buf).OnInit(null,null,testm);

            return (BuffBase)buf;
        }
    }
}
