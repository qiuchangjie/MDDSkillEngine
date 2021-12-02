using MDDGameFramework;

namespace MDDGameFramework
{
    public interface INPBehaveManager 
    {

        void SetBehaveHelper(IBehaveHelper behaveHelper);

        Clock GetClock();

        Blackboard GetSharedBlackboard(string key);

        NP_Tree CreatBehaviourTree(string Name,object userData);
    }
}


