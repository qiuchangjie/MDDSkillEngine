using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameStateCtrl ctrl = new GameStateCtrl();
        Debug.LogError("1111111");
        ctrl.Initialize();
        ctrl.GotoState("LoginState");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
