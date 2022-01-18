
using Sirenix.OdinInspector;
using MDDGameFramework.Runtime;
using MDDGameFramework;

namespace MDDSkillEngine
{
    [HideLabel]
    public class NP_WaitNodeData : NP_NodeDataBase
    {
        [HideInEditorMode] private Wait m_Wait;

        public ClassForBlackboard blackboardData = new ClassForBlackboard();

        public override Task CreateTask(object owner, NP_Tree runtimeTree)
        {
            m_Wait = Wait.Create(blackboardData.BBKey);
            return m_Wait;
        }

        public override Node NP_GetNode()
        {
            return this.m_Wait;
        }
    }
}