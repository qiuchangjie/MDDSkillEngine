using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using NPBehave;

namespace NPBehave.node
{
	public class RootNode : BehaviourNode
	{ 

		public Root root;

		[Output] 
		public bool output;
		[Input]
		public bool input;

		// Use this for initialization
		protected override void Init()
		{
			base.Init();
			root = new Root(false);
		}

        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from, to);

			((RootNode)from.node).root.SetMainNode(((ActionTest)to.node).action);

			root.SetDecoratee();

			root.SetRoot(root);

			
		}

        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
		{
			//Debug.LogError("wuhu");
			return null; // Replace this
		}
	}
}

