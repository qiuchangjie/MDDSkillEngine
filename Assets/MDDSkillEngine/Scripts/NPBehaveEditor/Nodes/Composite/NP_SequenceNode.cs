using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace NPBehave.node
{
    public class NP_SequenceNode : NP_CompositeNodeBase
    {
        public override string Name => "Sequence";


        NP_SequenceNodeData data = new NP_SequenceNodeData();
       

       
        public override NP_NodeDataBase NP_GetNodeData()
        {
            return data;
        }
    }
}



