using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState : IState
{
    public virtual void OnStateEnter()
    {
        
    }

    public virtual void OnStateLeave()
    {
        
    }

    public virtual void OnStateOverride()
    {
        
    }

    public virtual void OnStateResume()
    {
        
    }

    public virtual string name { get { return GetType().Name; } }
}
