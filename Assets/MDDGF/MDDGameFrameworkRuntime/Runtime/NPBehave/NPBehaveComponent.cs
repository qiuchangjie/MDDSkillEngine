using MDDGameFramework.Runtime;

namespace MDDGameFramework.Runtime
{
    public class NPBehaveComponent : MDDGameFrameworkComponent
    {
        private INPBehaveManager m_NPBehaveManager;

        protected override void Awake()
        {
            base.Awake();

            m_NPBehaveManager = MDDGameFrameworkEntry.GetModule<INPBehaveManager>();
            if (m_NPBehaveManager == null)
            {
                Log.Fatal("MDDGameFramework.Runtime manager is invalid.");
                return;
            }
        }

        private void Start()
        {
        }

        public Clock GetClock()
        {
            return m_NPBehaveManager.GetClock();
        }

        public Root CreatBehaviourTree(Node mainNode)
        {
            return m_NPBehaveManager.CreatBehaviourTree(mainNode);
        }

        public Blackboard GetSharedBlackboard(string key)
        {
            return m_NPBehaveManager.GetSharedBlackboard(key);
        }
    }
}
    


