using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using XNode;
using System.Collections.Generic;
using MDDSkillEngine;

namespace Slate
{

    [AddComponentMenu("SLATE/Play Cutscene On Click")]
    public class PlayCutsceneOnClick : MonoBehaviour
    {
        public ScriptableObject graph;
        public NPBehaveGraph graph1;

        public Cutscene cutscene;
        public float startTime;
        public UnityEvent onFinish;

        public void Start()
        {

            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                graph1 = graph as NPBehaveGraph;
                Debug.LogError(graph.name);

                f1(graph1.nodes[0]);
            }
        }

        void OnMouseDown()
        {

            graph1 = graph as NPBehaveGraph;
            Debug.LogError(graph.name);

            f1(graph1.nodes[0]);


            if (cutscene == null)
            {
                Debug.LogError("Cutscene is not provided", gameObject);
                return;
            }

            cutscene.Play(startTime, () => { onFinish.Invoke(); });
        }

        void Reset()
        {
            var collider = GetComponent<Collider>();
            if (collider == null)
            {
                collider = gameObject.AddComponent<BoxCollider>();
            }
        }

        public static GameObject Create()
        {
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.name = "Cutscene Click Trigger";
            go.AddComponent<PlayCutsceneOnClick>();
            return go;
        }

        /// <summary>
        /// 实现函数1
        /// 将多叉树中的节点，按照深度进行输出
        /// 实质上实现的是层次优先遍历
        /// </summary>
        /// <param name="head">首节点</param>
        private static void f1(Node head)
        {
            Node tMTreeNode;
            Queue<Node> queue = new Queue<Node>(100); //将队列初始化大小为100
            Stack<Node> stack = new Stack<Node>(100); //将栈初始化大小为100
            //head.level = 0; //根节点的深度为0

            //将根节点入队列
            queue.Enqueue(head);

            //对多叉树中的节点的深度值level进行赋值
            //采用层次优先遍历方法，借助于队列
            while (queue.Count != 0) //如果队列q不为空
            {
                tMTreeNode = queue.Dequeue(); //出队列

                foreach (var v in tMTreeNode.Outputs)
                {
                    foreach (var v1 in v.GetConnections())
                    {
                        queue.Enqueue(v1.node);
                    }
                }

                //for (int i = 0; i < tMTreeNode.NChildren; i++)
                //{
                //    //tMTreeNode.Children[i].Level = tMTreeNode.level + 1; //对子节点深度进行赋值：父节点深度加1
                //    queue.Enqueue(tMTreeNode.Children[i]); //将子节点入队列
                //}
                stack.Push(tMTreeNode); //将p入栈
            }

            while (stack.Count != 0) //不为空
            {
                tMTreeNode = stack.Pop(); //弹栈
                Debug.LogError(tMTreeNode.name);
                //System.Diagnostics.Debug.WriteLine("   {0} {1}", tMTreeNode.Level, tMTreeNode.Name);
            }
        }
    }
}