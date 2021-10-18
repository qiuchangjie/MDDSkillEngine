using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace NPBehave.node
{
    public class NP_Selector : NP_CompositeNodeBase
    {
        public override string Name => "Selector";

        public Selector m_SelectorNode;

        public override Composite CreateCompositeNode(NP_Tree runtimeTree, Node[] node)
        {
            this.m_SelectorNode = new Selector(node);
            return this.m_SelectorNode;
        }

        public override Node NP_GetNode()
        {
            return this.m_SelectorNode;
        }
    }
}



