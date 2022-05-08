
using Sirenix.OdinInspector;
using MDDGameFramework.Runtime;
using MDDGameFramework;

namespace MDDSkillEngine
{
    [HideLabel]
    public class NP_WaitUntilStopNodeData : NP_NodeDataBase
    {
        public bool ifSucess;

        [HideInEditorMode] private WaitUntilStopped m_WaitUntilStopped;
       
        public override Task CreateTask(object owner, NP_Tree runtimeTree)
        {
            m_WaitUntilStopped = WaitUntilStopped.Create(ifSucess);
            return m_WaitUntilStopped;
        }

        public override Node NP_GetNode()
        {
            return this.m_WaitUntilStopped;
        }
    }
}