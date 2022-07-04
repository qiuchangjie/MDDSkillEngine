using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    [Skill]
    public class kaerdiancimaichongState : SkillTimelineState<Entity>
    {
        protected override void OnInit(IFsm<Entity> fsm)
        {
            base.OnInit(fsm);
            
           
        }

        protected override void OnEnter(IFsm<Entity> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("{0}进入{1}状态", LogConst.FSM, GetType().Name);
        }

        protected override void OnDestroy(IFsm<Entity> fsm)
        {
            base.OnDestroy(fsm);
        }

        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            Log.Info("{0}离开{1}状态", LogConst.FSM, GetType().Name);
        }

        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 状态跳转
        /// 基于黑板的观察函数
        /// </summary>
        /// <param name="type"></param>
        /// <param name="newValue"></param>
        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
            VarBoolean varBoolean = (VarBoolean)newValue;

            if (varBoolean.Value == false)
                return;

            if (((MDDFsmState<Entity>)Fsm.CurrentState).StateType == StateType.IDLE)
            {
                ISkillSystem skillSystem = Game.Skill.GetSkillSystem(Fsm.Owner.Id);
                skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.PROGRESS);
                ChangeState(Fsm,GetType());
            }
            else
            {
                ISkillSystem skillSystem = Game.Skill.GetSkillSystem(Fsm.Owner.Id);
                skillSystem.SetSkillReleaseResultType(SkillReleaseResultType.FAIL);
            }
        }
    }
}

