using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace MDDSkillEngine
{
    public class InputSystemFuckTest : MonoBehaviour
    {
        Gamepad gamepad;
        // Start is called before the first frame update
        void Start()
        {
           //手柄
             gamepad = Gamepad.current;
        }

        // Update is called once per frame
        void Update()
        {

            if (gamepad != null)
            {
                Debug.Log(gamepad.leftStick.ReadValue());//手柄遥感的偏移
                if (gamepad.bButton.wasPressedThisFrame)
                    Debug.Log("按下B键");
            }

        }
    }
}


