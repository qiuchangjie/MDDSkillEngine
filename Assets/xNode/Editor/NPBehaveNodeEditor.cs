using UnityEditor;
using UnityEngine;
using XNode;
using XNode.Examples.MathNodes;
using XNodeEditor;

namespace NPBehave.node
{
  
    [CustomNodeEditor(typeof(NP_NodeBase))]
    public class NPBehaveNodeEditor : NodeEditor
    {    
        public override void OnBodyGUI()
        {
            if (target == null)
            {
                Debug.LogWarning("Null target node for node editor!");
                return;
            }
            NodePort input = target.GetPort("input");
            NodePort output = target.GetPort("output");

            GUILayout.BeginHorizontal();
            if (input != null) NodeEditorGUILayout.PortField(GUIContent.none, input, GUILayout.MinWidth(0));
            if (output != null) NodeEditorGUILayout.PortField(GUIContent.none, output, GUILayout.MinWidth(0));
            GUILayout.EndHorizontal();

            EditorGUIUtility.labelWidth = 60;

            //base.OnBodyGUI();                   
        }

      
    }
}