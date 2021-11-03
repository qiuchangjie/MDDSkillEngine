using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using NPBehave;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace NPBehave.node
{
	public class ActionTest : NP_NodeBase
	{

		public Action action;

		[Input]
		public bool input;

		// Use this for initialization
		protected override void Init()
		{
			base.Init();
			action = new Action();

			action.SetAction(debug);
		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}

		public void debug()
		{
			Debug.LogError("fuckTEst");
		}

        public override NP_NodeDataBase NP_GetNodeData()
        {
            throw new System.NotImplementedException();
        }
    }
}

