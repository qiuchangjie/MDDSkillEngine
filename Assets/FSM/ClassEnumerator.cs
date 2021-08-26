using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ClassEnumerator
{
    protected List<Type> Results = new List<Type>();
    public List<Type> results { get { return Results; } }

    public ClassEnumerator(Type InAttributeType,Type InInterfaceType,Assembly assembly)
    {
        Type[] Types = assembly.GetTypes();

        if (Types != null)
        {
            for (int i = 0; i < Types.Length; i++)
            {
                var v = Types[i];
                if (InInterfaceType.IsAssignableFrom(v))
                {
                    if (!v.IsAbstract)
                    {
                        if (v.GetCustomAttributes(InAttributeType, false).Length > 0)
                        {
                            Results.Add(v);
                        }
                    }
                }
            }
        }
    }

}
