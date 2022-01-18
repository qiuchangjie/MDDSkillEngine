using Sirenix.OdinInspector;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    [HideLabel]
    public class NP_BlackBoardConditionNodeData : NP_NodeDataBase
    {
        BlackboardCondition m_BlackboardCondition;

        [BoxGroup("黑板条件节点配置")]
        [LabelText("运算符号")]
        public Operator Ope = Operator.IS_EQUAL;

        [BoxGroup("黑板条件节点配置")]
        [LabelText("终止条件")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;

        public ClassForBlackboard blackboardData = new ClassForBlackboard();


        public override Decorator CreateDecoratorNode(object m_object, NP_Tree runtimeTree, Node node)
        {
            this.m_BlackboardCondition = BlackboardCondition.Create(blackboardData.BBKey, Ope, blackboardData.NP_BBValue,Stop, node);
            return this.m_BlackboardCondition;
        }

        public override Node NP_GetNode()
        {
            return this.m_BlackboardCondition;
        }
    }
}


