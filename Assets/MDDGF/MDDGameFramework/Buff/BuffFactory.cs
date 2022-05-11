using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace MDDGameFramework
{
    public static class BuffFactory
    {
        public static IBuffFactory s_BuffFactoryHelper;

        public static void SetBuffFactoryHelper(IBuffFactory buffFactoryHelper)
        {
            s_BuffFactoryHelper = buffFactoryHelper;
        }

        public static BuffBase AcquireBuff(string bufName,object Target, object From,object userData=null)
        {
            return s_BuffFactoryHelper.AcquireBuff(bufName, Target, From, userData);
        }
       
    }
}
