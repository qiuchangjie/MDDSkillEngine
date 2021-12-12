using OdinSerializer;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MDDGameFramework
{
    public static partial class Utility
    {
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCopy<T>(this T obj)
            where T : class
        {
            try
            {
                if (obj == null)
                {
                    return null;
                }

                byte[] bytes = SerializationUtility.SerializeValue(obj, DataFormat.Binary);

                return SerializationUtility.DeserializeValue<T>(bytes, DataFormat.Binary);             
            }
            catch (Exception e)
            {          
                return null;
            }
        }

        public static Variable VDeepCopy(this Variable obj)
        {
            Variable var = ReferencePool.Acquire(obj.GetType()) as Variable;

            if (var == null)
            {
                throw new MDDGameFrameworkException("引用池子深拷贝失败！！");
            }

            var.SetValue(obj.GetValue());

            return var;
        }


        public static T DeepCopyByReflect<T>(T obj)
        {
            //如果是字符串或值类型则直接返回
            if (obj is string || obj.GetType().IsValueType) return obj;

            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                try { field.SetValue(retval, DeepCopyByReflect(field.GetValue(obj))); }
                catch { }
            }
            return (T)retval;
        }
    }
}
