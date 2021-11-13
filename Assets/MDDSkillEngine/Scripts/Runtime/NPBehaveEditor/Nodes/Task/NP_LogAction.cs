using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace NPBehave.node
{
    public class NP_LogAction : NP_TaskNodeBase
    {
        public override string Name => "NP_LogAction";


        public NP_ActionNodeData data = new NP_ActionNodeData()
        {
            NpClassForAction = new MDDGameFramework.Runtime.NP_LogAction()
        };


        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }
    }
}



