using NPBehave;

namespace MDDGameFramework
{
    public interface INPBehaveManager 
    {
        Clock GetClock();

        Blackboard GetSharedBlackboard(string key);

        Root CreatBehaviourTree(Node mainNode);
    }
}


