using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    [AiState]
    public class AiDiedState : MDDFsmState<Entity>
    {
        private ClipState.Transition died;

        IFsm<Entity> Fsm;

        protected override void OnInit(IFsm<Entity> fsm)
        {
            Log.Error("创建死亡状态");
            base.OnInit(fsm);
            Fsm = fsm;
            died = fsm.Owner.CachedAnimContainer.GetAnimation("died");
            fsm.SetData<VarBoolean>("died", false);
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            Log.Error("进入死亡状态");
            base.OnEnter(fsm);
            fsm.Owner.CachedAnimancer.Play(died);
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
    }
}

