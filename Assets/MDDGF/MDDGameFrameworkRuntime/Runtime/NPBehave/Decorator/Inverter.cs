using MDDGameFramework;

namespace MDDGameFramework.Runtime
{
    public class Inverter : Decorator
    {
        public Inverter(Node decoratee) : base("Inverter", decoratee)
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
            Stopped(!result);
        }
    }
}