using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{

    public class NP_SelectorNodeData : NP_NodeDataBase
    {
        Selector m_Selector;

        public override Composite CreateComposite(Node[] node)
        {
            this.m_Selector = Selector.Create(node);
            return this.m_Selector;
        }

        public override Node NP_GetNode()
        {
            return this.m_Selector;
        }
    }
}


