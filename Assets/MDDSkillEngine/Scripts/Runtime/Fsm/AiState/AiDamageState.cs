using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    [AiState]
    public class AiDamageState : FsmState<Enemy>
    {
        private ClipState.Transition Damage;

        private System.Action endAction;

        protected override void OnInit(IFsm<Enemy> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建ai   damage模式 ");
            Damage = fsm.Owner.CachedAnimContainer.GetAnimation("damage");
            fsm.SetData<VarBoolean>("damage", false);

           

            endAction += () =>
            {
                fsm.SetData<VarBoolean>("damage", false);
                Finish(fsm);
            };

            Damage.Events.OnEnd += endAction;
        }

        protected override void OnEnter(IFsm<Enemy> fsm)
        {
            base.OnInit(fsm);
            fsm.Owner.CachedAnimancer.Play(Damage);
        }

        protected override void OnDestroy(IFsm<Enemy> fsm)
        {
            base.OnDestroy(fsm);
        }

        protected override void OnLeave(IFsm<Enemy> fsm, bool isShutdown)
        {
            Damage.Events.OnEnd -= endAction;
            base.OnLeave(fsm, isShutdown);
            Log.Error("离开ai伤害状态");
        }

        protected override void OnUpdate(IFsm<Enemy> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
          
        }
    }
}

