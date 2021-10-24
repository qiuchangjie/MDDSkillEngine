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



        //public Parallel m_ParallelNode;

        //public Parallel.Policy SuccessPolicy = Parallel.Policy.ALL;

        //public Parallel.Policy FailurePolicy = Parallel.Policy.ALL;

        //public override Composite CreateCompositeNode(MDDGameFramework.NP_Tree runtimeTree, Node[] node)
        //{
        //    this.m_ParallelNode = Parallel.Create(SuccessPolicy, FailurePolicy,node);
        //    return this.m_ParallelNode;
        //}

        //public override Node NP_GetNode()
        //{
        //    return this.m_ParallelNode;
        //}
    }
}



