using UnityEngine;
using NPBehave;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using MDDSkillEngine;
using System.Collections.Generic;

public class NPBehaveExampleHelloBlackboardsAI : MonoBehaviour
{
    private Root behaviorTree;

    VarBoolean var = true;

    System.Action action;

    public NPBehaveGraph np;

    public void addint()
    {

    }

    void Start()
    {
        //InitTree();

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
                    new BlackboardCondition("foo", Operator.IS_EQUAL, (VarBoolean)true, Stops.IMMEDIATE_RESTART,

                        // when 'toggled' is true, this branch will get executed.
                        new Sequence(

                            // print out a message ...
                            new Action(action
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
        debugger.BehaviorTree = root;
#endif
    }

    Root root;

    //public void InitTree()
    //{
    //    foreach (var v in np.nodes)
    //    {
    //        NP_NodeBase data = v as NP_NodeBase;

    //        switch (data.nodeType)
    //        {
    //            case NodeType.Task:
    //                data.NP_GetNodeData().CreateTask(null, null);
    //                break;
    //            case NodeType.Decorator:
    //                Node node = null;
    //                foreach (var v1 in data.Outputs)
    //                {
    //                    node = (v1.Connection.node as NP_NodeBase).NP_GetNodeData().NP_GetNode();
    //                }

    //                data.NP_GetNodeData().CreateDecoratorNode(null, null, node);

    //                break;
    //            case NodeType.Composite:
    //                List<Node> nodes = new List<Node>();
    //                foreach (var v1 in data.Outputs)
    //                {
    //                    foreach (var v2 in v1.GetConnections())
    //                    {
    //                        nodes.Add((v2.node as NP_NodeBase).NP_GetNodeData().NP_GetNode());
    //                    }
    //                }
    //                data.NP_GetNodeData().CreateComposite(nodes.ToArray());
    //                break;
    //        }

    //        if (data.Name == "根节点")
    //        {
    //            root = data.NP_GetNodeData().NP_GetNode() as Root;
    //        }


    //    }

    //    root.SetRoot(root);

    //    root.Start();
    //}

}
