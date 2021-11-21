using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    [AiState]
    public class AiIdleState : FsmState<Enemy>
    {
        private ClipState.Transition idle;

        public override bool StrongState
        {
            get
            {
                return true;
            }
        }


        protected override void OnInit(IFsm<Enemy> fsm)
        {
            base.OnInit(fsm);
            idle = fsm.Owner.CachedAnimContainer.GetAnimation("idle");
            fsm.SetData<VarBoolean>("idle", false);
        }

        protected override void OnEnter(IFsm<Enemy> fsm)
        {
            base.OnInit(fsm);
            fsm.Owner.CachedAnimancer.Play(idle);
        }

        protected override void OnDestroy(IFsm<Enemy> fsm)
        {
            base.OnDestroy(fsm);
        }

        protected override void OnLeave(IFsm<Enemy> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnUpdate(IFsm<Enemy> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            if (fsm.GetData<VarBoolean>("damage"))
            {
                ChangeState<AiDamageState>(fsm);
            }

            if (fsm.GetData<VarBoolean>("died"))
            {
                ChangeState<AiDiedState>(fsm);
            }

        }
    }
}

