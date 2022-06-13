using MDDGameFramework;
using System.Collections.Generic;
using MDDSkillEngine;

namespace MDDGameFramework.Runtime
{
    public class NPHasTarget : ObservingDecorator
    {
        private Dictionary<string, Variable> Conditions;

        public NPHasTarget() { }


        public NPHasTarget(Stops stopsOnChange, Node decoratee) : base("NPHasTarget", stopsOnChange, decoratee)
        {

        }

        public static NPHasTarget Create( Stops stopsOnChange, Node decoratee)
        {
            NPHasTarget NPHasTarget = ReferencePool.Acquire<NPHasTarget>();
            return NPHasTarget;
        }

        public override void Clear()
        {
            base.Clear();
            Conditions = null;
            Decoratee = null;
            isObserving = false;
        }

        /// <summary>
        /// 暂时用于kaer的祈祷召唤 不需要时时观测数据变化
        /// </summary>
        override protected void StartObserving()
        {

        }

        override protected void StopObserving()
        {

        }

        private void onValueChanged(Blackboard.Type type, object newValue)
        {
            
        }

        protected override bool IsConditionMet()
        {
            if (Game.Select.attackTarget != null)
            {
                return true;
            }
            return false;
        }

        override public string ToString()
        {
            string keys = "";
            foreach (var key in Conditions)
            {
                keys += " " + key.Key;
            }
            return Name + keys;
        }
    }
}