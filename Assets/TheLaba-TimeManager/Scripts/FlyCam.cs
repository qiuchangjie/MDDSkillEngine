using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyCam : MonoBehaviour
{

    //this scripts allows you to use the fly cam to navigate in the scene

    public float speed = 50.0f; //regular speed
    public float shiftAdd = 100.0f; //speed with shift clicked
    public float maxShift = 160.0f;    // max speed with 
    private Vector3 lstMousPos = new Vector3(255, 255, 255); //checl last position of the mouse
    private float sensivity = 0.15f;      //sensivity speed

    private float totalRun = 1.0f;

    void Update()           //manage the cam movement
    {
        lstMousPos = Input.mousePosition - lstMousPos;
        lstMousPos = new Vector3(-lstMousPos.y * sensivity, lstMousPos.x * sensivity, 0);
        lstMousPos = new Vector3(transform.eulerAngles.x + lstMousPos.x, transform.eulerAngles.y + lstMousPos.y, 0);
        transform.eulerAngles = lstMousPos;
        lstMousPos = Input.mousePosition;
  
        float f = 0.0f;
        Vector3 pos = GetBaseInput();
        if (Input.GetKey(KeyCode.LeftShift))
        {
            totalRun += Time.deltaTime;
            pos = pos * totalRun * shiftAdd;
            pos.x = Mathf.Clamp(pos.x, -maxShift, maxShift);
            pos.y = Mathf.Clamp(pos.y, -maxShift, maxShift);
            pos.z = Mathf.Clamp(pos.z, -maxShift, maxShift);
        }
        else
        {
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            pos = pos * speed;
        }

        pos = pos * Time.deltaTime;
         transform.Translate(pos);
        

    }

    private Vector3 GetBaseInput()          //get the input of the WASD 
    {  
        Vector3 posSpeed = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            posSpeed += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            posSpeed += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            posSpeed += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            posSpeed += new Vector3(1, 0, 0);
        }
        return posSpeed;
    }
}
