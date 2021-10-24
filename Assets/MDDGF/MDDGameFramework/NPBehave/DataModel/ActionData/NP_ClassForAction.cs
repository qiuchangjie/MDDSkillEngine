using System;
using Sirenix.OdinInspector;


namespace MDDGameFramework
{
    [BoxGroup("用于包含Action的数据类"), GUIColor(0.961f, 0.902f, 0.788f, 1f)]
    [HideLabel]
    public class NP_ClassForAction
    {
        /// <summary>
        /// 归属的对象
        /// </summary>
        [HideInEditorMode] public object owner;

        /// <summary>
        /// 归属的运行时行为树实例
        /// </summary>
        [HideInEditorMode]
        public NP_Tree BelongtoRuntimeTree;

        [HideInEditorMode]
        public System.Action Action;

        [HideInEditorMode]
        public Func<bool> Func1;

        [HideInEditorMode]
        public Func<bool, Action.Result> Func2;

        /// <summary>
        /// 获取将要执行的委托函数，也可以在这里面做一些初始化操作
        /// </summary>
        /// <returns></returns>
        public virtual System.Action GetActionToBeDone()
        {
            return null;
        }

        public virtual System.Func<bool> GetFunc1ToBeDone()
        {
            return null;
        }

        public virtual System.Func<bool, Action.Result> GetFunc2ToBeDone()
        {
            return null;
        }

        public  Action _CreateNPBehaveAction()
        {
            Action action;

            GetActionToBeDone();
            if (this.Action != null)
            {
                action = ReferencePool.Acquire<Action>();
                action.SetAction(Action);
                return action;
            }

            GetFunc1ToBeDone();
            if (this.Func1 != null)
            {
                action = ReferencePool.Acquire<Action>();
                action.SetSingleFunc(Func1);
                return action;
            }

            GetFunc2ToBeDone();
            if (this.Func2 != null)
            {
                action = ReferencePool.Acquire<Action>();
                action.SetFunc1(Func2);
                return action;
            }
           
            return null;
        }
    }
}