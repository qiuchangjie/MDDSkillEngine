using MDDGameFramework;


namespace MDDGameFramework.Runtime
{
    public class WaitUntilStopped : Task
    {
        private bool sucessWhenStopped;

        public WaitUntilStopped() { }
       
        public WaitUntilStopped(bool sucessWhenStopped = false) : base("WaitUntilStopped")
        {
            this.sucessWhenStopped = sucessWhenStopped;
        }

        public static WaitUntilStopped Create(bool sucessWhenStopped = false)
        {
            WaitUntilStopped waitUntilStopped = ReferencePool.Acquire<WaitUntilStopped>();

            waitUntilStopped.sucessWhenStopped = sucessWhenStopped;

            return waitUntilStopped;
        }


        protected override void DoStop()
        {
            this.Stopped(sucessWhenStopped);
        }

        public override void Clear()
        {
            base.Clear();
            sucessWhenStopped = false;
        }
    }
}