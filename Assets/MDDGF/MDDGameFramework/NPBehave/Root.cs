using UnityEngine.Assertions;

namespace MDDGameFramework
{
    public class Root : Decorator
    {
        private Node mainNode;

        //private Node inProgressNode;

        private Blackboard blackboard;
        public override Blackboard Blackboard
        {
            get
            {
                return blackboard;
            }
        }


        private Clock clock;
        public override Clock Clock
        {
            get
            {
                return clock;
            }          
        }

        System.Action actionCache;

#if UNITY_EDITOR
        public int TotalNumStartCalls = 0;
        public int TotalNumStopCalls = 0;
        public int TotalNumStoppedCalls = 0;
#endif

        public Root()        
        { 
        }

        public Root(bool isEditor) : base("Root")
        {
             //this.clock = UnityContext.GetClock();
             //this.blackboard = new Blackboard(this.clock);
            //Decoratee = this;
            //this.SetRoot(this);
        }

        public void SetClock()
        {
            this.clock = UnityContext.GetClock();
            this.blackboard = new Blackboard(this.clock);
        }

        public void SetMainNode(Node mainNode)
        {
            this.mainNode = mainNode;
        }

        public void SetDecoratee()
        {
            Decoratee = this.mainNode;
        }

        public Root(Node mainNode) : base("Root", mainNode)
        {
            this.mainNode = mainNode;
            this.clock = UnityContext.GetClock();
            this.blackboard = new Blackboard(this.clock);
            this.SetRoot(this);
            //actionCache = Start;
        }
        public Root(Blackboard blackboard, Node mainNode) : base("Root", mainNode)
        {
            this.blackboard = blackboard;
            this.mainNode = mainNode;
            this.clock = UnityContext.GetClock();
            this.SetRoot(this);
            //actionCache = Start;
        }

        public Root(Blackboard blackboard, Clock clock, Node mainNode) : base("Root", mainNode)
        {
            this.blackboard = blackboard;
            this.mainNode = mainNode;
            this.clock = clock;
            this.SetRoot(this);
        }

        public static Root Create(Node mainNode)
        {
            Root root = ReferencePool.Acquire<Root>();
            root.Name = "Root";
            root.mainNode = mainNode;
            root.clock = UnityContext.GetClock();
            root.blackboard = new Blackboard(root.clock);


            return root;
        }

        public override void Clear()
        {
            base.Clear();
            mainNode = null;
            clock = null;
            blackboard = null;
            
        }

        public override void SetRoot(Root rootNode)
        {
            Assert.AreEqual(this, rootNode);
            base.SetRoot(rootNode);
            this.mainNode.SetRoot(rootNode);
        }


        override protected void DoStart()
        {
            this.blackboard.Enable();
            this.mainNode.Start();
        }

        override protected void DoStop()
        {
            if (this.mainNode.IsActive)
            {
                this.mainNode.Stop();
            }
            else
            {
                this.clock.RemoveTimer(this.mainNode.startCache);
            }
        }


        override protected void DoChildStopped(Node node, bool success)
        {
            if (!IsStopRequested)
            {
                // wait one tick, to prevent endless recursions
                this.clock.AddTimer(0, 0, this.mainNode.startCache);
            }
            else
            {
                this.blackboard.Disable();
                Stopped(success);
            }
        }
    }
}
