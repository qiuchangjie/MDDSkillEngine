using MDDGameFramework;
using System.Collections.Generic;

namespace MDDGameFramework.Runtime
{
    public class AllTrueblackBoard : ObservingDecorator
    {
        private Dictionary<string, Variable> Conditions;

        public AllTrueblackBoard() { }
        

        public AllTrueblackBoard(Dictionary<string, Variable> Conditions, Stops stopsOnChange, Node decoratee) : base("AllTrueblackBoard", stopsOnChange, decoratee)
        {
            this.Conditions = Conditions;
        }

        public static AllTrueblackBoard Create(Dictionary<string, Variable> Conditions, Stops stopsOnChange, Node decoratee)
        {
            AllTrueblackBoard allTrueblackBoard = ReferencePool.Acquire<AllTrueblackBoard>();
            allTrueblackBoard.Conditions = Conditions;
            allTrueblackBoard.Decoratee = decoratee;
            allTrueblackBoard.stopsOnChange = stopsOnChange;
            allTrueblackBoard.isObserving = false;
            allTrueblackBoard.Decoratee.SetParent(allTrueblackBoard);

            return allTrueblackBoard;
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
            //Evaluate();
        }

        protected override bool IsConditionMet()
        {
            int i = Conditions.Count;
            //条件达成的数量
            int j = 0;

            foreach (var v in Conditions)
            {
                Variable boardValue = Blackboard.Get(v.Key);

                if (Equals(boardValue.GetValue(),v.Value.GetValue()))
                {
                    j++;
                }
            }

            if (j == i)
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