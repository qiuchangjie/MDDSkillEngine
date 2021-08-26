using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[GameState]
public class LoginState : BaseState
{
    public override void OnStateEnter()
    {
        Debug.LogError("进入loginState");
    }

    public override void OnStateLeave()
    {
        Debug.LogError("退出loginState");
    }
}
