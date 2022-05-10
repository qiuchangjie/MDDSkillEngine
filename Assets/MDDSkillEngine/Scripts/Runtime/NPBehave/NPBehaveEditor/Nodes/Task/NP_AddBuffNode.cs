using MDDGameFramework;
using MDDGameFramework.Runtime;
using MDDSkillEngine;

namespace MDDSkillEngine
{
    public class NP_AddBuffNode : NP_TaskNodeBase
    {
        public override string Name => "添加buff节点";


        public NP_ActionNodeData data = new NP_ActionNodeData()
        {
            NpClassForAction = new NP_AddBuffAction()
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



