using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class NP_SelectorNode : NP_CompositeNodeBase
    {
        public override string Name => "Selector";

        Selector m_SelectorNode;

        public override Composite CreateCompositeNode(NP_Tree runtimeTree, Node[] node)
        {
            this.m_SelectorNode = Selector.Create(node); 
            return this.m_SelectorNode;
        }

       
        public override NP_NodeDataBase NP_GetNodeData()
        {
            throw new System.NotImplementedException();
        }
    }
}



