using MDDGameFramework;
using MDDGameFramework.Runtime;
using MDDSkillEngine;

namespace MDDSkillEngine
{
    public class NP_ResetCDNode : NP_TaskNodeBase
    {
        public override string Name => "NP_ResetCDNode";


        public NP_ActionNodeData data = new NP_ActionNodeData()
        {
            NpClassForAction = new NP_ResetCDAction()
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



