using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimeManager : MonoBehaviour
{
    //this is the main script tha allows you to manage the global time 
    public float slowDownFactor = 0.05f;            //factor to how much slow time
    public float fastFactor = 2;                    //factor to how much speed up time

    void Start()                                //set the time normal at the beginning
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
       
    }
    public void DoSlowMotion()              //fuction to do slow motion
    {
        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;

    }
    public void DoRealtime()                //function to back to normal time
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f;
    }
    public void DoFastMotion()                  //fuction to do fast motion (time lapse)
    {
        Time.timeScale = fastFactor;
        Time.fixedDeltaTime = 0.02f;
    }

    public void StopTime()                  //faction to pause the time
    {
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0.02f;
    }

}
