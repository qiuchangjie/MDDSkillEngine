using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MDDGameFramework
{
    public static partial class Utility
    {
        /// <summary>
        /// 程序集相关的实用函数。
        /// </summary>
        public static class Assembly
        {
            private static readonly System.Reflection.Assembly[] s_Assemblies = null;
            private static readonly Dictionary<string, Type> s_CachedTypes = new Dictionary<string, Type>(StringComparer.Ordinal);
            private static System.Reflection.Assembly runTimeAssembly = null;
            private static Type[] runTimeTypes = null;

            static Assembly()
            {
                s_Assemblies = AppDomain.CurrentDomain.GetAssemblies();
            }

            /// <summary>
            /// 获取已加载的程序集。
            /// </summary>
            /// <returns>已加载的程序集。</returns>
            public static System.Reflection.Assembly[] GetAssemblies()
            {
                return s_Assemblies;
            }

            public static System.Reflection.Assembly GetRuntimeAssembly()
            {
                if (runTimeAssembly == null)
                {
                    for (int i = 0; i < s_Assemblies.Length; i++)
                    {
                        if (s_Assemblies[i].GetName().Name == "MDDSkillEngine")
                        {
                            runTimeAssembly = s_Assemblies[i];
                            break;
                        }
                    }

                    runTimeTypes = runTimeAssembly.GetTypes();
                }

                return runTimeAssembly;
            }

            /// <summary>
            /// 获取已加载的程序集中的所有类型。
            /// </summary>
            /// <returns>已加载的程序集中的所有类型。</returns>
            public static Type[] GetTypes()
            {
                List<Type> results = new List<Type>();
                foreach (System.Reflection.Assembly assembly in s_Assemblies)
                {
                    results.AddRange(assembly.GetTypes());
                }

                return results.ToArray();
            }

            /// <summary>
            /// 获取已加载的程序集中的所有类型。
            /// </summary>
            /// <param name="results">已加载的程序集中的所有类型。</param>
            public static void GetTypes(List<Type> results)
            {
                if (results == null)
                {
                    throw new MDDGameFrameworkException("Results is invalid.");
                }

                results.Clear();
                foreach (System.Reflection.Assembly assembly in s_Assemblies)
                {
                    results.AddRange(assembly.GetTypes());
                }
            }

            /// <summary>
            /// 获取已加载的程序集中的指定类型。
            /// </summary>
            /// <param name="typeName">要获取的类型名。</param>
            /// <returns>已加载的程序集中的指定类型。</returns>
            public static Type GetType(string typeName)
            {
                if (string.IsNullOrEmpty(typeName))
                {
                    throw new MDDGameFrameworkException("Type name is invalid.");
                }

                Type type = null;
                if (s_CachedTypes.TryGetValue(typeName, out type))
                {
                    return type;
                }

                type = Type.GetType(typeName);
                if (type != null)
                {
                    s_CachedTypes.Add(typeName, type);
                    return type;
                }

                foreach (System.Reflection.Assembly assembly in s_Assemblies)
                {
                    type = Type.GetType(Text.Format("{0}, {1}", typeName, assembly.FullName));
                    if (type != null)
                    {
                        s_CachedTypes.Add(typeName, type);
                        return type;
                    }
                }

                return null;
            }


            /// <summary>
            /// 通过父类获取类型
            /// </summary>
            /// <param name="results"></param>
            /// <param name="fatherType"></param>
            public static void GetTypesByFather(List<Type> results, Type fatherType)
            {
                if (results == null)
                {
                    throw new MDDGameFrameworkException("Results is invalid.");
                }

                if (runTimeAssembly == null)
                {
                    for (int i = 0; i < s_Assemblies.Length; i++)
                    {
                        if (s_Assemblies[i].GetName().Name == "MDDSkillEngine")
                        {
                            runTimeAssembly = s_Assemblies[i];
                            break;
                        }
                    }

                    runTimeTypes = runTimeAssembly.GetTypes();
                }

                for (int i = 0; i < runTimeTypes.Length; i++)
                {
                    if (fatherType.IsAssignableFrom(runTimeTypes[i]))
                    {
                        if (!runTimeTypes[i].IsAbstract)
                        {
                            results.Add(runTimeTypes[i]);
                        }
                    }
                }
            }


            /// <summary>
            /// 通过标签获取runtime程序集中的指定类型
            /// </summary>
            /// <typeparam name="TAttribute"></typeparam>
            /// <param name="results"></param>
            public static void GetTypesByAttribute<TAttribute>(List<Type> results,Type interfaceType) where TAttribute : Attribute
            {
                if (results == null)
                {
                    throw new MDDGameFrameworkException("Results is invalid.");
                }

                Type attributeType = typeof(TAttribute);

                if (runTimeAssembly == null)
                {
                    for (int i = 0; i < s_Assemblies.Length; i++)
                    {
                        if (s_Assemblies[i].GetName().Name == "MDDSkillEngine")
                        {
                            runTimeAssembly = s_Assemblies[i];
                            break;
                        }
                    }

                    runTimeTypes = runTimeAssembly.GetTypes();
                }

                for (int i = 0; i < runTimeTypes.Length; i++)
                {
                    if (interfaceType.IsAssignableFrom(runTimeTypes[i]))
                    {
                        if (!runTimeTypes[i].IsAbstract)
                        {
                            if (runTimeTypes[i].GetCustomAttributes(attributeType, false).Length > 0)
                            {
                                results.Add(runTimeTypes[i]);
                            }
                        }
                    }                 
                }
            }
        }
    }
}
