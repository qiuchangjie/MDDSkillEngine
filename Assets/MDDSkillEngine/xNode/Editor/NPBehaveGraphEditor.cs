using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;
using static XNodeEditor.NodeGraphEditor;
using MDDSkillEngine;
using MDDGameFramework;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;

namespace NPBehave.node
{
	[CustomNodeGraphEditor(typeof(NPBehaveGraph))]
	public class NPBehaveGraphEditor : NodeGraphEditor
	{
		NPBehaveNodeMenuTree treeWindow;
		NPBehaveGraph graph;

        Queue<XNode.Node> queue = new Queue<XNode.Node>(100); //将队列初始化大小为100
        Stack<XNode.Node> stack = new Stack<XNode.Node>(100);

        NP_NodeBase nP_DragNode;

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

        public override void OnGUI()
        {
            base.OnGUI();
            //DragAndDropUtilities.DrawDropZone(window.position, nP_DragNode, null, 1);
            //nP_DragNode = DragAndDropUtilities.DragAndDropZone(window.position, nP_DragNode, typeof(NP_NodeBase), true,
            //    true) as NP_NodeBase;
        }

        public override void OnDropObjects(Object[] objects)
        {
            base.OnDropObjects(objects);
            Debug.LogError(objects[0].name);
        }

        /// <summary> 
        /// Overriding GetNodeMenuName lets you control if and how nodes are categorized.
        /// In this example we are sorting out all node types that are not in the XNode.Examples namespace.
        /// </summary>
        public override string GetNodeMenuName(System.Type type)
		{
			if (type.Namespace == "MDDSkillEngine")
			{
				return base.GetNodeMenuName(type).Replace("NPBehave/node", "");
			}
			else return null;
		}

		public void OnChange(XNode.Node node)
		{
            Sort();
        }

        /// <summary>
        /// 行为树排序方法
        /// </summary>
        public void Sort()
        {
            if (graph == null)
                return;

            SortAllChildrenNode();
            SortTree(graph.GetRootNode());
        }


        /// <summary>
        /// 给所有的孩子节点排序 通过坐标确定顺序
        /// </summary>
        private void SortAllChildrenNode()
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

        /// <summary>
        /// 行为树排序 并给ID自动赋值 层次遍历
        /// </summary>
        /// <param name="head"></param>
        private void SortTree(XNode.Node head)
        {
            if (head == null)
            {
                Debug.LogError("根节点为空");
                return;
            }   
            
            XNode.Node tMTreeNode;
            
            //将根节点入队列
            queue.Enqueue(head);

            //采用层次优先遍历方法，借助于队列
            while (queue.Count != 0) //如果队列q不为空
            {
                tMTreeNode = queue.Dequeue(); //出队列

                //孩子入队
                foreach (var v in tMTreeNode.Outputs)
                {
                    foreach (var v1 in v.GetConnections())
                    {
                        queue.Enqueue(v1.node);
                    }
                }

                stack.Push(tMTreeNode); //将p入栈
            }

            int i = 0;

            while (stack.Count != 0) //不为空
            {
                i++;
                tMTreeNode = stack.Pop(); //弹栈
                NP_NodeBase node = tMTreeNode as NP_NodeBase;
                node.Id = i;
            }

            graph.nodes.Sort((x,y)=> 
            {
                NP_NodeBase nodex = x as NP_NodeBase;
                NP_NodeBase nodey = y as NP_NodeBase;

                return nodex.Id.CompareTo(nodey.Id);
            });
        }

    }
}