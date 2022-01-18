using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{

    public class NP_PalelNodeData : NP_NodeDataBase
    {
        public Parallel m_ParallelNode;

        public Parallel.Policy SuccessPolicy = Parallel.Policy.ALL;

        public Parallel.Policy FailurePolicy = Parallel.Policy.ALL;

        public override Composite CreateComposite(Node[] node)
        {
            this.m_ParallelNode = Parallel.Create(SuccessPolicy, FailurePolicy, node);
            return this.m_ParallelNode;
        }

        public override Node NP_GetNode()
        {
            return this.m_ParallelNode;
        }
    }
}


