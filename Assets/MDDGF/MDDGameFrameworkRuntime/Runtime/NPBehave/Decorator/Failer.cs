using MDDGameFramework;

namespace MDDGameFramework.Runtime
{
    public class Failer : Decorator
    {
        public Failer(Node decoratee) : base("Failer", decoratee)
        {
        }

        protected override void DoStart()
        {
            Decoratee.Start();
        }

        override protected void DoStop()
        {
            Decoratee.Cancel();
        }

        protected override void DoChildStopped(Node child, bool result)
        {
            Stopped(false);
        }
    }

}