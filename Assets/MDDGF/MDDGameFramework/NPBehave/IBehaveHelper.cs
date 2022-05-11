

namespace MDDGameFramework
{
    public interface IBehaveHelper
    {        
        NP_Tree CreatBehaviourTree (string Name , object userData,NPType nPType = NPType.skill);

        void PreLoad();
        
    }
}
