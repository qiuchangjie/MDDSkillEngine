using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using MDDGameFramework;


namespace MDDGameFramework.Runtime
{
    public class Selector : Composite
    {
        private int currentIndex = -1;

        public Selector()
        {
            
        }

        public Selector(params Node[] children) : base("Selector", children)
        {
        }

        public static Selector Create(params Node[] children)
        {
            Selector selector = ReferencePool.Acquire<Selector>();
            selector.Name = "Selector";
            selector.Children = children;

            foreach (Node node in selector.Children)
            {
                node.SetParent(selector);
            }

            return selector;
        }

        public override void Clear()
        {
            base.Clear();
            Name = null;
            Children = null;
            currentIndex = -1;
        }


        protected override void DoStart()
        {
            foreach (Node child in Children)
            {
                Assert.AreEqual(child.CurrentState, State.INACTIVE);
            }

            currentIndex = -1;

            ProcessChildren();
        }

        protected override void DoStop()
        {
            Children[currentIndex].Cancel();
        }

        protected override void DoChildStopped(Node child, bool result)
        {
            if (result)
            {
                Stopped(true);
            }
            else
            {
                ProcessChildren();
            }
        }

        private void ProcessChildren()
        {
            if (++currentIndex < Children.Length)
            {
                if (IsStopRequested)
                {
                    Stopped(false);
                }
                else
                {
                    Children[currentIndex].Start();
                }
            }
            else
            {
                Stopped(false);
            }
        }

        public override void StopLowerPriorityChildrenForChild(Node abortForChild, bool immediateRestart)
        {
            int indexForChild = 0;
            bool found = false;
            foreach (Node currentChild in Children)
            {
                if (currentChild == abortForChild)
                {
                    found = true;
                }
                else if (!found)
                {
                    indexForChild++;
                }
                else if (found && currentChild.IsActive)
                {
                    if (immediateRestart)
                    {
                        currentIndex = indexForChild - 1;
                    }
                    else
                    {
                        currentIndex = Children.Length;
                    }
                    currentChild.Cancel();
                    break;
                }
            }
        }

        override public string ToString()
        {
            return base.ToString() + "[" + this.currentIndex + "]";
        }
    }
}