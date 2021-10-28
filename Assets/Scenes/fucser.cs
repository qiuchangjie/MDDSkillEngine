using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fucser : MonoBehaviour
{

    public ScriptableObject scriptable;

    ScriptableObject scriptable1 = null;

    fuck fuck;

    fuck copy;

    // Start is called before the first frame update
    void Start()
    {
        fuck = new fuck();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            copy = fuck.DeepCopy();


            Log.Error("fuck.c:{0},fuck.b{1},fuck.fuck2.a{2}", copy.c, copy.b, copy.fuck2.a);
        }
    }
}


public class fuck
{
    public string c;
    public int b;
    public fuck2 fuck2;

    public fuck()
    {
        c = "123123123";
        b = 88;
        fuck2 = new fuck2
        {
            a = "123123"
        ,
            b = 99
        };
    }
}

public class fuck2
{
    public string a;
    public int b;

}