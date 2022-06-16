using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    [AkiState]
    public class AkiRunState : MDDFsmState<Entity>
    {
        private ClipState.Transition Run;

        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);
            Log.Info("创建akiRun状态。");
            Run = fsm.Owner.CachedAnimContainer.GetAnimation("Run");
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            //base.OnEnter(fsm);
            Log.Info("进入akiRun状态。");
            //播放跑步动画
            fsm.Owner.CachedAnimancer.Play(Run);            
        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
            Log.Info("销毁akiRun状态。");
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("离开akiRun状态。");
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);

            //根据寻路数据位移
            fsm.Owner.CacheMove.Moving(elapseSeconds);
        }

        /// <summary>
        /// 跳转凭据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="newValue"></param>
        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
            VarBoolean varBoolean = (VarBoolean)newValue;

            //如果为false 且还处在run状态则结束run状态
            if (varBoolean.Value == false)
            {
                if (Fsm.CurrentState == this)
                    ChangeState(Fsm,typeof(AkiIdleState));

                return;
            }

            Log.Info("{0}----------------------{1}---{2}",LogConst.FSM,GetType().Name, varBoolean.Value);
            
            if (Fsm.CurrentState == this)
            {
                return;
            }

            //可以根据需求自定义条件
            ChangeState(Fsm, GetType());
        }
    }
}

