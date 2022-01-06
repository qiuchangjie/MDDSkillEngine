using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRaycast : MonoBehaviour
{
        //this script allows you to click the object and call their fuction 
    void Update()
    {
        if (Input.GetMouseButtonDown(0))        //when click the mouse
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 50f))
            {
                if (hit.collider.gameObject.GetComponent<ObjectTime>())
                {
                    if(!hit.collider.transform.GetChild(2).gameObject.active)           
                    {
                        hit.collider.gameObject.GetComponent<ObjectTime>().StopMotion();        //stop the motion and open the time control
                        hit.collider.gameObject.GetComponent<ObjectTime>().OpenTimeControl();
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponent<ObjectTime>().CloseTimeControl();      //when you click it again close the options
                    }
                    
                }

            }
        }
    }
}
