using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    [AiState]
    public class AiDiedState : MDDFsmState<Entity>
    {
        private ClipState.Transition died;


        protected override void OnInit(IFsm<Entity> fsm)
        {
            Log.Info("{0}创建{1}", LogConst.FSM, GetType().Name);
            base.OnInit(fsm);
            died = fsm.Owner.CachedAnimContainer.GetAnimation("die");
            fsm.SetData<VarBoolean>("died", false);
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            Log.Info("{0}进入{1}", LogConst.FSM, GetType().Name);
            base.OnEnter(fsm);
            fsm.Owner.CachedAnimancer.Play(died);
        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            Log.Info("{0}销毁{1}", LogConst.FSM, GetType().Name);
            base.OnDestroy(fsm);
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            Log.Info("{0}离开{1}", LogConst.FSM, GetType().Name);
            base.OnLeave(fsm, isShutdown);
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            Log.Info("{0}更新{1}", LogConst.FSM, GetType().Name);
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

        }

        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
            
        }
    }
}

