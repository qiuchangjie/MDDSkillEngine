using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class NP_CooldownNode : NP_DecoratorNodeBase
    {
        public override string Name => "冷却时间";

        public NP_CooldownNodeData data = new NP_CooldownNodeData();
        
        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }
    }
}



