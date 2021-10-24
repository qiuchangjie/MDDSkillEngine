
using Sirenix.OdinInspector;

namespace MDDGameFramework.Runtime
{
    [BoxGroup("行为结点数据")]
    [HideLabel]
    public class NP_ActionNodeData : NP_NodeDataBase
    {
        [HideInEditorMode] private Action m_ActionNode;

        public NP_ClassForAction NpClassForAction;

        public override Task CreateTask(object owner, NP_Tree runtimeTree)
        {
            NpClassForAction.owner = owner;
            NpClassForAction.BelongtoRuntimeTree = runtimeTree;
            m_ActionNode = NpClassForAction._CreateNPBehaveAction();
            return m_ActionNode;
        }

        public override Node NP_GetNode()
        {
            return this.m_ActionNode;
        }
    }
}