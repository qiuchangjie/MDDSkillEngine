using MDDGameFramework;
using System.Collections.Generic;

namespace MDDGameFramework
{
    internal sealed class NPBehaveManager : MDDGameFrameworkModule, INPBehaveManager
    {
        private Dictionary<string, Blackboard> blackboards;
        private Dictionary<NameNamePair, NP_Tree> behaviourTreeDic;
        private Clock clock;

        private IBehaveHelper behaveHelper;


        public NPBehaveManager()
        {
            blackboards = new Dictionary<string, Blackboard>();
            behaviourTreeDic = new Dictionary<NameNamePair, NP_Tree>();
            clock = new Clock();
        }

     
        public void SetBehaveHelper(IBehaveHelper behaveHelper)
        {
            if (behaveHelper == null)
            {
                throw new MDDGameFrameworkException("behaveHelper helper is invalid.");
            }
          
            this.behaveHelper = behaveHelper;
        }

        public NP_Tree CreatBehaviourTree(string Name,object userData = null)
        {
            NP_Tree root = behaveHelper.CreatBehaviourTree(Name,userData);

            if (root == null)
            {
                throw new MDDGameFrameworkException("root is invalid.");
            }

            if (root.Owner != null)
            {
                behaviourTreeDic.Add(new NameNamePair(Name, root.Owner.Id.ToString()), root);
                blackboards.Add(new NameNamePair(Name, root.Owner.Id.ToString()).ToString(), root.Blackboard);
            }
            else
            {
                behaviourTreeDic.Add(new NameNamePair(Name), root);
                blackboards.Add(new NameNamePair(Name).ToString(), root.Blackboard);
            }

            return root;
        }

        public Clock GetClock()
        {
            return clock;
        }

        public Blackboard GetSharedBlackboard(string key)
        {
            if (!blackboards.ContainsKey(key))
            {
                blackboards.Add(key, new Blackboard(clock));
            }

            return blackboards[key];
        }

        internal override void Shutdown()
        {

        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            clock.Update(elapseSeconds);
        }
    }
}
