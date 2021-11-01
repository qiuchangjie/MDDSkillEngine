using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class StateMachine 
{
    Dictionary<string, IState> _registedState = new Dictionary<string, IState>();
    
    public Dictionary<string, IState> registedState
    {
        get
        {
            return _registedState;
        }        
    }

    private Queue<IState> statesQueue = new Queue<IState>();

    public ClassEnumerator RegisterStateByAttritubes<TattributeType>(Assembly assembly) where TattributeType : GameStateAttribute
    {
        var classes = new ClassEnumerator(typeof(TattributeType),typeof(IState),assembly);

        var iter = classes.results.GetEnumerator();

        while (iter.MoveNext())
        {
            var stateType = iter.Current;

            IState state  = (IState)System.Activator.CreateInstance(stateType);

            if (!_registedState.ContainsKey(stateType.Name))
            { 
                _registedState.Add(state.name,state);
            }
        }

        return classes;
    }

    public void GotoState(string name)
    {
        ChangeState(_registedState[name]);
    }

    private IState ChangeState(IState state)
    {
        if (state == null)
        {
            return default;
        }

        state.OnStateEnter();

        return default;
    }

}
