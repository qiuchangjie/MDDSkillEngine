using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateCtrl 
{
    private StateMachine gameState = new StateMachine();

    public void Initialize()
    {
        gameState.RegisterStateByAttritubes<GameStateAttribute>(typeof(GameStateAttribute).Assembly);
    }


    public void GotoState(string name)
    {
        gameState.GotoState(name);
    }

    //public IState GetState()
    //{
      
    //}
}
