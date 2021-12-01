using MDDGameFramework;

namespace MDDGameFramework
{
    public interface INPBehaveManager 
    {
        Clock GetClock();

        Blackboard GetSharedBlackboard(string key);

        Root CreatBehaviourTree(string Name,object userData);
    }
}


