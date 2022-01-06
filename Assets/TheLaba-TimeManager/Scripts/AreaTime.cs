using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTime : MonoBehaviour
{       //this script allow you to manage the objects that enter in this area
    public enum timeManager         //enum to choose waht the area does
    {
        FastMotion,
        SlowMotion,
        StopMotion
    };

    public timeManager timeType;            //reference to the area



    void OnTriggerEnter(Collider other)                 //manage trught the ontriggerenter 
    {
        if (other.gameObject.GetComponent<ObjectTime>())
        {
            
            switch (timeType)
            {
                case timeManager.FastMotion:
                    other.gameObject.GetComponent<ObjectTime>().DoFastMotion();
                    break;

                case timeManager.SlowMotion:
                    other.gameObject.GetComponent<ObjectTime>().DoSlowMotion();
                    break;

                case timeManager.StopMotion:
                    other.gameObject.GetComponent<ObjectTime>().StopMotion();
                    break;
                default: break;
            }
        }
    }

 
    void OnTriggerExit(Collider other)              //when the object exit call the normal speed
    {
        if (other.gameObject.GetComponent<ObjectTime>())
        {
             other.gameObject.GetComponent<ObjectTime>().BackMotion();
        }
    }
}
