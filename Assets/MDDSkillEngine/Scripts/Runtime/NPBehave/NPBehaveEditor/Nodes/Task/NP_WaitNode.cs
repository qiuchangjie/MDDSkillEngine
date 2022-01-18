using MDDGameFramework;
using MDDGameFramework.Runtime;
using MDDSkillEngine;

namespace MDDSkillEngine
{
    public class NP_WaitNode : NP_TaskNodeBase
    {
        public override string Name => "NP_Wait";


        public NP_WaitNodeData data = new NP_WaitNodeData()
        {
          
        };

       
        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }
    }
}



