using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace NPBehave.node
{
    public class NP_ParallelNode : NP_CompositeNodeBase
    {
        public override string Name => "Parallel";

        NP_PalelNodeData data = new NP_PalelNodeData();

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }
    }
}



