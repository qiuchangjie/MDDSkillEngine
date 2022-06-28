using MDDGameFramework.Runtime;
using System;
using UnityEngine;
using MDDGameFramework;

namespace MDDGameFramework.Runtime
{
    public class NPBehaveComponent : MDDGameFrameworkComponent
    {
        [SerializeField]
        private string m_NPBehaveHelperTypeName = "MDDSkillEngine.NPBehaveHelper";

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


            InitBehaveHelperHelper();
        }

        private void Start()
        {
        }


        public IBehaveHelper GetHelper()
        {
            return m_NPBehaveManager.GetHelper();
        }

        private void InitBehaveHelperHelper()
        {
            if (string.IsNullOrEmpty(m_NPBehaveHelperTypeName))
            {
                return;
            }

            Type BehaveHelperType = Utility.Assembly.GetType(m_NPBehaveHelperTypeName);
            if (BehaveHelperType == null)
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("Can not find log helper type '{0}'.", m_NPBehaveHelperTypeName));
            }

            IBehaveHelper BehaveHelper = (IBehaveHelper)Activator.CreateInstance(BehaveHelperType);
            if (BehaveHelper == null)
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("Can not create log helper instance '{0}'.", m_NPBehaveHelperTypeName));
            }

            m_NPBehaveManager.SetBehaveHelper(BehaveHelper);
        }


        public Clock GetClock()
        {
            return m_NPBehaveManager.GetClock();
        }

        public void RemoveBehaviourTree(NameNamePair nameNamePair)
        {
            m_NPBehaveManager.RemoveBehaviourTree(nameNamePair);
        }

        public NP_Tree CreatBehaviourTree(string name, object userdata)
        {
            return m_NPBehaveManager.CreatBehaviourTree(name, userdata);
        }

        //public Blackboard GetSharedBlackboard(string key)
        //{
        //    return m_NPBehaveManager.GetSharedBlackboard(key);
        //}
    }
}
    


