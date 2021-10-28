using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;
using static XNodeEditor.NodeGraphEditor;
using MDDSkillEngine;
using MDDGameFramework;
using Sirenix.OdinInspector;

namespace NPBehave.node
{
	[CustomNodeGraphEditor(typeof(NPBehaveGraph))]
	public class NPBehaveGraphEditor : NodeGraphEditor
	{
		NPBehaveNodeMenuTree treeWindow;

		NPBehaveGraph graph;
        public override void OnOpen()
        {
            base.OnOpen();
            treeWindow = NPBehaveNodeMenuTree.OpenWindow();
            treeWindow.Show();
            LayoutUtility.DockEditorWindow(window, treeWindow);

            NodeEditor.onUpdateNode = OnChange;

            graph = target as NPBehaveGraph;
            if (graph != null)
            {
                NPBlackBoardEditorInstance.BBValues = graph.BBValues;
            }
        }

        public override void OnDropObjects(Object[] objects)
        {
            base.OnDropObjects(objects);
			Debug.LogError(objects.Length);
        }

        //public override void OnGUI()
        //{
        //    base.OnGUI();
        //    if (GUI.Button(window.position,GUIContent.none))
        //    {
        //        SortAllChildrenNode();
        //    }
        //}

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

		public void OnChange(XNode.Node node)
		{
            SortAllChildrenNode();

        }

        [Button("Sort")]
        public void SortAllChildrenNode()
        {
            if (graph == null)
                return;

            foreach (var node in graph.nodes)
            {
                foreach (var port in node.Outputs)
                {
                    port.Sort();
                }
            }
        }

	}
}