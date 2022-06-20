using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    [AiState]
    public class AiIdleState : MDDFsmState<Entity>
    {
        private ClipState.Transition idle;

        public override bool IsButtomState
        {
            get
            {
                return true;
            }
        }


        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);
           
            idle = fsm.Owner.CachedAnimContainer.GetAnimation("Idle_A");
            fsm.SetData<VarBoolean>("idle", false);
            
           
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            base.OnEnter(fsm);
            fsm.Owner.CachedAnimancer.Play(idle);
        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        }

        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
            VarBoolean varBoolean = (VarBoolean)newValue;

            if (varBoolean.Value == false)
                return;
            

            Finish(Fsm);
         
        }
    }
}

