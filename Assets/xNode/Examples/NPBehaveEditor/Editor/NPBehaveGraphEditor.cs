using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode.Examples;
using XNode;
using XNodeEditor;

namespace NPBehave.node
{
	[CustomNodeGraphEditor(typeof(MathGraphEditor))]
	public class MathGraphEditor : NodeGraphEditor
	{

		/// <summary> 
		/// Overriding GetNodeMenuName lets you control if and how nodes are categorized.
		/// In this example we are sorting out all node types that are not in the XNode.Examples namespace.
		/// </summary>
		public override string GetNodeMenuName(System.Type type)
		{
			if (type.Namespace == "NPBehave.node")
			{
				return base.GetNodeMenuName(type).Replace("NPBehave/node", "");
			}
			else return null;
		}
	}
}