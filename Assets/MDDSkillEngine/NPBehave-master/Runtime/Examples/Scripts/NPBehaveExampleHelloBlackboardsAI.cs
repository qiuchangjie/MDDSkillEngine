﻿using UnityEngine;
using NPBehave;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using MDDSkillEngine;

public class NPBehaveExampleHelloBlackboardsAI : MonoBehaviour
{
    private Root behaviorTree;

    VarBoolean var = true;

    System.Action action;

    public void addint()
    {
        
    }

    void Start()
    {
        action = addint;

        behaviorTree = new Root(

            // toggle the 'toggled' blackboard boolean flag around every 500 milliseconds
            new Service(0.5f, () =>
            {

                UnityEngine.Profiling.Profiler.BeginSample("xiaofang");
                behaviorTree.Blackboard.Set<VarBoolean>("foo", !behaviorTree.Blackboard.Get<bool>("foo"));
                UnityEngine.Profiling.Profiler.EndSample();
            },

                new Selector(

                    // Check the 'toggled' flag. Stops.IMMEDIATE_RESTART means that the Blackboard will be observed for changes 
                    // while this or any lower priority branches are executed. If the value changes, the corresponding branch will be
                    // stopped and it will be immediately jump to the branch that now matches the condition.
                    new BlackboardCondition("foo", Operator.IS_EQUAL, var, Stops.IMMEDIATE_RESTART,

                        // when 'toggled' is true, this branch will get executed.
                        new Sequence(

                            // print out a message ...
                            new Action( action
                                             ),

                            // ... and stay here until the `BlackboardValue`-node stops us because the toggled flag went false.
                            new WaitUntilStopped()
                        )
                    ),

                    // when 'toggled' is false, we'll eventually land here
                    new Sequence(
                        new Action(action),
                        new WaitUntilStopped()
                    )
                )
            )
        );
        behaviorTree.Start();

        // attach the debugger component if executed in editor (helps to debug in the inspector) 
#if UNITY_EDITOR
        Debugger debugger = (Debugger)this.gameObject.AddComponent(typeof(Debugger));
        debugger.BehaviorTree = behaviorTree;
#endif
    }
}
