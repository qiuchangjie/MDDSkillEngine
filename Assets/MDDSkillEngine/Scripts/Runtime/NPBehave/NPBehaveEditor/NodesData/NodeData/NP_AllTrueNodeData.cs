using Sirenix.OdinInspector;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections.Generic;

namespace MDDSkillEngine
{
    [HideLabel]
    public class NP_AllTrueNodeData : NP_NodeDataBase
    {
        AllTrueblackBoard m_BlackboardCondition;

        [BoxGroup("黑板条件节点配置")]
        [LabelText("终止条件")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;

        [LabelText("条件1")]
        public ClassForBlackboard blackboardData1 = new ClassForBlackboard();
        [LabelText("条件2")]
        public ClassForBlackboard blackboardData2 = new ClassForBlackboard();
        [LabelText("条件3")]
        public ClassForBlackboard blackboardData3 = new ClassForBlackboard();


        public override Decorator CreateDecoratorNode(object m_object, NP_Tree runtimeTree, Node node)
        {
            Dictionary<string,Variable> vars = new Dictionary<string,Variable>();

            vars.Add(blackboardData1.BBKey, blackboardData1.NP_BBValue);
            vars.Add(blackboardData2.BBKey, blackboardData1.NP_BBValue);
            vars.Add(blackboardData3.BBKey, blackboardData1.NP_BBValue);

            this.m_BlackboardCondition = AllTrueblackBoard.Create(vars, Stop, node);
            return this.m_BlackboardCondition;
        }

        public override Node NP_GetNode()
        {
            return this.m_BlackboardCondition;
        }
    }
}


