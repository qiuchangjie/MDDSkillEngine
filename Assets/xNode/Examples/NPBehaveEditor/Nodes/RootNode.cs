using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using NPBehave;

namespace NPBehave.node
{
	public class RootNode : XNode.Node
	{

		[Output] 
		public int Root;


		// Use this for initialization
		protected override void Init()
		{
			base.Init();

		}

		// Return the correct value of an output port when requested
		public override object GetValue(NodePort port)
		{
			return null; // Replace this
		}
	}
}

