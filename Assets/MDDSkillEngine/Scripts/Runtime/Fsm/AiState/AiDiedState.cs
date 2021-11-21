using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    [AiState]
    public class AiDiedState : FsmState<Enemy>
    {
        private ClipState.Transition died;

        protected override void OnInit(IFsm<Enemy> fsm)
        {
            Log.Error("创建死亡状态");
            base.OnInit(fsm);
            died = fsm.Owner.CachedAnimContainer.GetAnimation("died");
            fsm.SetData<VarBoolean>("died", false);
        }

        protected override void OnEnter(IFsm<Enemy> fsm)
        {
            Log.Error("进入死亡状态");
            base.OnInit(fsm);
            fsm.Owner.CachedAnimancer.Play(died);
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

        }
    }
}

