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

        public PathFindingTest findingTest;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

            animancers = GetComponent<AnimancerComponent>();
            findingTest = GetComponent<PathFindingTest>();

        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

           
        }

        


    }
}


