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

    public static FsmComponent Fsm
    {
        get;
        private set;
    }

    public static EventComponent Event
    {
        get;
        private set;
    }

    public static ObjectPoolComponent ObjectPool
    {
        get;
        private set;
    }

    public static EntityComponent Entity
    {
        get;
        private set;
    }


    private void InitMDDComponent()
    {
        Buff = MDDGameEntry.GetComponent<BuffComponent>();
        Fsm = MDDGameEntry.GetComponent<FsmComponent>();
        Event = MDDGameEntry.GetComponent<EventComponent>();
        ObjectPool = MDDGameEntry.GetComponent<ObjectPoolComponent>();
        Entity = MDDGameEntry.GetComponent<EntityComponent>();
    }
}
