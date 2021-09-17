using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using NPBehave;

namespace NPBehave.node
{
	public abstract class NP_NodeBase : XNode.Node
	{
	    public Node node;

		public abstract void Initialize();
	}
}

