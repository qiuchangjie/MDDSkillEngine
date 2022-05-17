using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;

namespace MDDSkillEngine
{
    public class UIBlackboard : UGuiForm
    {
        private Blackboard m_blackboard;

        private List<Blackboard> m_Child = new List<Blackboard>();
        private System.Action<Blackboard.Type, Variable> actionCache;

        public Dictionary<string, UIKeyValue> UIkeyvalue = new Dictionary<string, UIKeyValue>();

        public Transform instanceTransform;
        public UIKeyValue instance;

        public void InitData(Blackboard blackboard)
        {
            List<string> keys = blackboard.Keys;
            foreach (string key in keys)
            {
                AddKeyvalue(key, blackboard.Get(key), blackboard);
            }
        }


        public void AddKeyvalue(string key, Variable value, Blackboard blackboard = null)
        {
            UIKeyValue uIKeyValue = Instantiate(instance, instanceTransform);
            UIkeyvalue.Add(key, uIKeyValue);
            uIKeyValue.key.text = key;
            if (value.GetValue() != null)
                uIKeyValue.value.text = value.GetValue().ToString();
            else
                uIKeyValue.value.text = " ";

            //添加观察者 bind数据变化检测
            if (blackboard != null)
            {
                blackboard.AddObserver(key, (a, b) =>
                {
                    if (value.GetValue() != null)
                        uIKeyValue.value.text = b.GetValue().ToString();
                });
            }
        }


    }

}

