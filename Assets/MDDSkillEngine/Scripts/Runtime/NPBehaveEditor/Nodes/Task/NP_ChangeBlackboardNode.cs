using MDDGameFramework;
using MDDGameFramework.Runtime;
using MDDSkillEngine;

namespace MDDSkillEngine
{
    public class NP_ChangeBlackboardNode : NP_TaskNodeBase
    {
        public override string Name => "修改黑板值";


        public NP_ChangeBlackboardNodeData data = new NP_ChangeBlackboardNodeData()
        {
            NpClassForAction = new NP_ChangeBlackboardAction()
        };

        public override Task CreateTask(NP_Tree owner_Tree)
        {
            return data.NpClassForAction._CreateNPBehaveAction();
        }

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }
    }
}



