using MDDGameFramework;
using MDDGameFramework.Runtime;
using MDDSkillEngine;

namespace MDDSkillEngine
{
    public class NP_WaitUntilStoppedNode : NP_TaskNodeBase
    {

        public override string Name => "WaitUntilStopped";


        public NP_WaitUntilStopNodeData data = new NP_WaitUntilStopNodeData()
        {

        };

        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }
    }
}



