using MDDGameFramework;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace MDDSkillEngine
{
    [CustomNodeGraphEditor(typeof(NPBehaveGraph))]
	public class NPBehaveGraphEditor : NodeGraphEditor
	{
		NPBehaveNodeMenuTree treeWindow;
		NPBehaveGraph graph;

        Queue<XNode.Node> queue = new Queue<XNode.Node>(100); //将队列初始化大小为100
        Stack<XNode.Node> stack = new Stack<XNode.Node>(100);


        public override void OnOpen()
        {
            base.OnOpen();

            this.window.titleContent = new GUIContent("行为树编辑面板");

            treeWindow = NPBehaveNodeMenuTree.OpenWindow();
            treeWindow.Show();

            
            LayoutUtility.DockEditorWindow(window, treeWindow);


            graph = target as NPBehaveGraph;
            if (graph != null)
            {
                //获取该行为树的黑板值
                NPBlackBoardEditorInstance.BBValues = graph.BBValues;
               
                KaelBlackboard kaelBlackboard= graph.PublicBB as KaelBlackboard;

                //将公共黑板数据填充进选项
                foreach (var v in kaelBlackboard.BBValues)
                {
                    if (!NPBlackBoardEditorInstance.AllBB.TryGetValue(v.Key,out Variable variable))
                    {
                        NPBlackBoardEditorInstance.AllBB.Add(v.Key,v.Value);
                    }
                }

                //将黑板数据填充进选项
                foreach (var v in NPBlackBoardEditorInstance.BBValues)
                {                  
                    if (!NPBlackBoardEditorInstance.AllBB.TryGetValue(v.Key, out Variable variable))
                    {
                        NPBlackBoardEditorInstance.AllBB.Add(v.Key, v.Value);
                    }
                }

                //通过反射获取所有buff的名字
                List<Type> types = new List<Type>();
                Utility.Assembly.GetTypesByFather(types, typeof(BuffBase));
                List<string> buffsName = new List<string>();
                foreach (var type in types)
                {
                    buffsName.Add(type.Name);
                }
                NPBlackBoardEditorInstance.buffs = buffsName;

            }

            treeWindow.setins(graph);
        }

        public override void OnGUI()
        {
            base.OnGUI();
            Event e = Event.current;
            int cid = GUIUtility.GetControlID(FocusType.Passive);
            switch (e.GetTypeForControl(cid))
            {
                case EventType.KeyDown:
                    if (e.keyCode == (KeyCode)113)
                    {
                        Debug.LogError("保存行为树数据");
                        Sort();
                        EditorUtility.SetDirty(graph);
                        EditorUtility.SetDirty(graph.PublicBB);
                        AssetDatabase.SaveAssets();
                    }
                    e.Use();
                    break;
            }
        }


        public override void OnDropObjects(UnityEngine.Object[] objects)
        {
            base.OnDropObjects(objects);
            NP_NodeBase nP = DragAndDrop.GetGenericData("dragflag") as NP_NodeBase;

            if (nP == null)
                return;

            Type type = nP.GetType();
            CreateNode(type, NodeEditorWindow.current.WindowToGridPosition(Event.current.mousePosition));
            Event.current.Use();
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