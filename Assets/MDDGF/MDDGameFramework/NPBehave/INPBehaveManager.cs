using MDDGameFramework;

namespace MDDGameFramework
{
    public interface INPBehaveManager 
    {

        IBehaveHelper GetHelper();

        void SetBehaveHelper(IBehaveHelper behaveHelper);

        Clock GetClock();

        Blackboard GetSharedBlackboard(string key);

        NP_Tree CreatBehaviourTree(string Name,object userData);

        void RemoveBehaviourTree(NameNamePair nameNamePair);
    }

    public enum NPType
    {
        skill,
        ai,

    }
}


