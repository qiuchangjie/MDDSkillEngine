using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class NP_SelectorNode : NP_CompositeNodeBase
    {
        public override string Name => "Selector";

        NP_SelectorNodeData data = new NP_SelectorNodeData();

           
        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }
    }
}



