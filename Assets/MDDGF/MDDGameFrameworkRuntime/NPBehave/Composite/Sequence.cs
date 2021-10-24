using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using MDDGameFramework;


namespace MDDGameFramework.Runtime
{
    public class Sequence : Composite
    {
        private int currentIndex = -1;

        public Sequence()
        {
            
        }

        public Sequence(params Node[] children) : base("Sequence", children)
        {
        }

        public static Sequence Create(params Node[] children)
        {
            Sequence sequence = ReferencePool.Acquire<Sequence>();

            sequence.Name = "Sequence";
            sequence.Children = children;

            return sequence;
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
            Children[currentIndex].Stop();
        }


        protected override void DoChildStopped(Node child, bool result)
        {
            if (result)
            {
                ProcessChildren();
            }
            else
            {
                Stopped(false);
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
                Stopped(true);
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
                    currentChild.Stop();
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