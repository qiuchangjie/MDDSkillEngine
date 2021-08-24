using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;

public class GameEnter : MonoBehaviour
{
    public void Start()
    {
        InitMDDComponent();
    }

    public static BuffComponent Buff
    {
        get;
        private set;
    }


    private void InitMDDComponent()
    {
        Buff = MDDGameEntry.GetComponent<BuffComponent>();
    }
}
