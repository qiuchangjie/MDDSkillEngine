using MDDGameFramework;
using System.Collections.Generic;

namespace MDDGameFramework
{
    internal sealed class NPBehaveManager : MDDGameFrameworkModule, INPBehaveManager
    {
        private Dictionary<string, Blackboard> blackboards;
        private Dictionary<int, Root> behaviourTreeDic;
        private Clock clock;

        public NPBehaveManager()
        {
            blackboards = new Dictionary<string, Blackboard>();
            behaviourTreeDic = new Dictionary<int, Root>();
            clock = new Clock();
        }


        public Root CreatBehaviourTree(Node mainNode)
        {
            Root tree = new Root(new Blackboard(clock),clock,mainNode);
            behaviourTreeDic.Add(1, tree);
            return tree;
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
