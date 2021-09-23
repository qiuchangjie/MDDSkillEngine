using MDDGameFramework;
using Animancer;

namespace MDDSkillEngine
{
    public class Enemy : TargetableObject
    {
        Blackboard blackboard;

        Blackboard shared_Blackboard;

        IFsm<Enemy> fsm;

        AnimancerComponent animancers;

        Root behaveTree;

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);


        }


    }
}


