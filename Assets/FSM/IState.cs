using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{

    /// <summary>
    /// 进入状态栈
    /// </summary>
    void OnStateEnter();


    /// <summary>
    /// 状态退出栈
    /// </summary>
    void OnStateLeave();


    /// <summary>
    /// 由栈顶变为非栈顶
    /// </summary>
    void OnStateOverride();


    /// <summary>
    /// 状态由非栈顶变为栈顶
    /// </summary>
    void OnStateResume();


    string name { get; }

}
