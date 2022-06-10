using MDDGameFramework;
using System;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public abstract class MDDFsmState<T> : FsmState<T> where T : Entity
    {
        protected IFsm<T> Fsm;
        protected float FinishTime = 0f;
        protected override void OnInit(IFsm<T> fsm)
        {
            base.OnInit(fsm);
            Fsm = fsm;
            fsm.SetData<VarBoolean>(GetType().Name, false);
            fsm.AddObserver(GetType().Name, Observing);
            Log.Info("{0}设置默认状态黑板变量{1}", LogConst.FSM, GetType().Name);
        }

        protected override void OnEnter(IFsm<T> fsm)
        {
            base.OnEnter(fsm);

            if (fsm.Owner == Game.Select.selectEntity)
            {
                if (SelectUtility.MouseRayCastByLayer(1 << 0 | 1 << 1, out RaycastHit vector3))
                {
                    Game.Select.currentMouse = vector3.point;
                    Game.Select.mouseTarget.transform.position = vector3.point;                 
                    fsm.Owner.CachedTransform.LookAt(Game.Select.currentMouse);
                    Game.Select.mouseTarget.transform.rotation = fsm.Owner.CachedTransform.rotation;
                }

                if (SelectUtility.MouseRayCastByLayer(1 << 8 | 1 << 11, out RaycastHit hit))
                {
                    MDDGameFramework.Runtime.Entity entity = hit.transform.GetComponent<MDDGameFramework.Runtime.Entity>();

                    if (entity != null)
                    {
                        Game.Select.mouseSeeEntity = entity.Logic as Entity;
                    }
                }
            }
        }

        protected override void OnLeave(IFsm<T> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);

            if (fsm.Owner == Game.Select.selectEntity)
            {
                Game.Select.mouseSeeEntity = null;
            }

            fsm.SetData<VarBoolean>(GetType().Name, false);
        }

        /// <summary>
        /// 状态跳转的依据函数
        /// </summary>
        /// <param name="type">黑板值变化类型</param>
        /// <param name="newValue">值</param>
        protected abstract void Observing(Blackboard.Type type, Variable newValue);


    }
}
