using UnityEngine;
using MDDGameFramework;


namespace MDDGameFramework.Runtime
{
    public class BlackboardCondition : ObservingDecorator
    {
        private string key;
        private Variable value;
        private Operator op;
        private System.Action<Blackboard.Type, Variable> actionCache;

        public string Key
        {
            get
            {
                return key;
            }
        }

        public Variable Value
        {
            get
            {
                return value;
            }
        }

        public Operator Operator
        {
            get
            {
                return op;
            }
        }

        public BlackboardCondition()
        {
            actionCache = onValueChanged;
        }
       
        public BlackboardCondition(string key, Operator op, Variable value, Stops stopsOnChange, Node decoratee) : base("BlackboardCondition", stopsOnChange, decoratee)
        {
            this.op = op;
            this.key = key;
            this.value = value ;
            this.stopsOnChange = stopsOnChange;
            actionCache = onValueChanged;
        }
        
        public BlackboardCondition(string key, Operator op, Stops stopsOnChange, Node decoratee) : base("BlackboardCondition", stopsOnChange, decoratee)
        {
            this.op = op;
            this.key = key;
            this.stopsOnChange = stopsOnChange;
            actionCache = onValueChanged;
        }

        public static BlackboardCondition Create(string key, Operator op, Variable value, Stops stopsOnChange, Node decoratee)
        {
            BlackboardCondition blackboardCondition = ReferencePool.Acquire<BlackboardCondition>();

            blackboardCondition.Name = "BlackboardCondition";
            blackboardCondition.op = op;
            blackboardCondition.key = key;
            blackboardCondition.value = value;
            blackboardCondition.stopsOnChange = stopsOnChange;
            blackboardCondition.Decoratee = decoratee;
            blackboardCondition.isObserving = false;
            blackboardCondition.Decoratee.SetParent(blackboardCondition);
            
            return blackboardCondition;
        }

        public override void Clear()
        {
            RootNode.Blackboard.RemoveObserver(key, actionCache);
            key = null;
            value = null;
            Decoratee = null;
            isObserving = false;       
            base.Clear();
        }


        override protected void StartObserving()
        {
            this.RootNode.Blackboard.AddObserver(key, actionCache);
        }

        override protected void StopObserving()
        {
            this.RootNode.Blackboard.RemoveObserver(key, actionCache);
        }

        private void onValueChanged(Blackboard.Type type, Variable newValue)
        {
            Evaluate();
        }

        override protected bool IsConditionMet()
        {
            if (op == Operator.ALWAYS_TRUE)
            {
                return true;
            }

            if (!this.RootNode.Blackboard.Isset(key))
            {
                return op == Operator.IS_NOT_SET;
            }

            Variable o = this.RootNode.Blackboard.Get(key);

            switch (this.op)
            {
                case Operator.IS_SET: return true;
                case Operator.IS_EQUAL: bool var = object.Equals(o.GetValue(), value.GetValue());
                    return var;

                case Operator.IS_NOT_EQUAL: return !object.Equals(o.GetValue(), value.GetValue());

                case Operator.IS_GREATER_OR_EQUAL:

                    if (o is VarFloat)
                    {
                        return ((VarFloat)o).Value >= ((VarFloat)this.value).Value;
                    }
                    else if (o is VarInt32)
                    {
                        return ((VarInt32)o).Value >= ((VarInt32)this.value).Value;
                    }
                    else
                    {
                        Debug.LogError("Type not compareable: " + o.GetType());
                        return false;
                    }

                case Operator.IS_GREATER:
                    if (o is VarFloat)
                    {
                        return ((VarFloat)o).Value > ((VarFloat)this.value).Value;
                    }
                    else if (o is VarInt32)
                    {
                        return ((VarInt32)o).Value > ((VarInt32)this.value).Value;
                    }
                    else
                    {
                        Debug.LogError("Type not compareable: " + o.GetType());
                        return false;
                    }

                case Operator.IS_SMALLER_OR_EQUAL:
                    if (o is VarFloat)
                    {
                        return ((VarFloat)o).Value <= ((VarFloat)this.value).Value;
                    }
                    else if (o is VarInt32)
                    {
                        return ((VarInt32)o).Value <= ((VarInt32)this.value).Value;
                    }
                    else
                    {
                        Debug.LogError("Type not compareable: " + o.GetType());
                        return false;
                    }

                case Operator.IS_SMALLER:
                    if (o is VarFloat)
                    {
                        return ((VarFloat)o).Value < ((VarFloat)this.value).Value;
                    }
                    else if (o is VarInt32)
                    {
                        return ((VarInt32)o).Value < ((VarInt32)this.value).Value;
                    }
                    else
                    {
                        Debug.LogError("Type not compareable: " + o.GetType());
                        return false;
                    }

                default: return false;
            }
        }

        override public string ToString()
        {
            return Utility.Text.Format("({0}){1}?{2}", this.op, this.key, this.value);
        }
    }
}