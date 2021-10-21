using UnityEngine;
using MDDGameFramework;


namespace MDDGameFramework.Runtime
{
    public class BlackboardCondition : ObservingDecorator
    {
        private string key;
        private Variable value;
        private Operator op;

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

        public BlackboardCondition(string key, Operator op, Variable value, Stops stopsOnChange, Node decoratee) : base("BlackboardCondition", stopsOnChange, decoratee)
        {
            this.op = op;
            this.key = key;
            this.value = value;
            this.stopsOnChange = stopsOnChange;
        }
        
        public BlackboardCondition(string key, Operator op, Stops stopsOnChange, Node decoratee) : base("BlackboardCondition", stopsOnChange, decoratee)
        {
            this.op = op;
            this.key = key;
            this.stopsOnChange = stopsOnChange;
        }


        override protected void StartObserving()
        {
            this.RootNode.Blackboard.AddObserver(key, onValueChanged);
        }

        override protected void StopObserving()
        {
            this.RootNode.Blackboard.RemoveObserver(key, onValueChanged);
        }

        private void onValueChanged(Blackboard.Type type, object newValue)
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

            object o = this.RootNode.Blackboard.Get(key);

            switch (this.op)
            {
                case Operator.IS_SET: return true;
                case Operator.IS_EQUAL: return object.Equals(o, value);
                case Operator.IS_NOT_EQUAL: return !object.Equals(o, value);

                case Operator.IS_GREATER_OR_EQUAL:
                    if (o is float)
                    {
                        return (float)o >= (VarInt64)this.value;
                    }
                    else if (o is int)
                    {
                        return (int)o >= (VarInt32)this.value;
                    }
                    else
                    {
                        Debug.LogError("Type not compareable: " + o.GetType());
                        return false;
                    }

                case Operator.IS_GREATER:
                    if (o is float)
                    {
                        return (float)o > (VarInt64)this.value;
                    }
                    else if (o is int)
                    {
                        return (int)o > (VarInt32)this.value;
                    }
                    else
                    {
                        Debug.LogError("Type not compareable: " + o.GetType());
                        return false;
                    }

                case Operator.IS_SMALLER_OR_EQUAL:
                    if (o is float)
                    {
                        return (float)o <= (VarInt64)this.value;
                    }
                    else if (o is int)
                    {
                        return (int)o <= (VarInt32)this.value;
                    }
                    else
                    {
                        Debug.LogError("Type not compareable: " + o.GetType());
                        return false;
                    }

                case Operator.IS_SMALLER:
                    if (o is float)
                    {
                        return (float)o < (VarInt64)this.value;
                    }
                    else if (o is int)
                    {
                        return (int)o < (VarInt32)this.value;
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
            return "(" + this.op + ") " + this.key + " ? " + this.value;
        }
    }
}