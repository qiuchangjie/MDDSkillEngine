using UnityEngine;
using System.Collections.Generic;

namespace MDDGameFramework
{
    public class Blackboard:IReference
    {
        public enum Type
        {
            ADD,
            REMOVE,
            CHANGE
        }
        private struct Notification
        {
            public string key;
            public Type type;
            public Variable value;
            public Notification(string key, Type type, Variable value)
            {
                this.key = key;
                this.type = type;
                this.value = value;
            }
        }

        private Clock clock;
        private Dictionary<string, Variable> data = new Dictionary<string, Variable>();
        private Dictionary<string, List<System.Action<Type, Variable>>> observers = new Dictionary<string, List<System.Action<Type, Variable>>>();
        private bool isNotifiyng = false;
        private Dictionary<string, List<System.Action<Type, Variable>>> addObservers = new Dictionary<string, List<System.Action<Type, Variable>>>();
        private Dictionary<string, List<System.Action<Type, Variable>>> removeObservers = new Dictionary<string, List<System.Action<Type, Variable>>>();
        private List<Notification> notifications = new List<Notification>();
        private List<Notification> notificationsDispatch = new List<Notification>();
        private Blackboard parentBlackboard;
        private HashSet<Blackboard> children = new HashSet<Blackboard>();

        private System.Action NotifiyObserversActionCache;

        public Blackboard()
        {
            NotifiyObserversActionCache = NotifiyObservers;
        }

        public Blackboard(Blackboard parent, Clock clock)
        {
            this.clock = clock;
            this.parentBlackboard = parent;
            NotifiyObserversActionCache = NotifiyObservers;
        }
        public Blackboard(Clock clock)
        {
            this.parentBlackboard = null;
            this.clock = clock;
            NotifiyObserversActionCache = NotifiyObservers;
        }

        public static Blackboard Create(Blackboard parent, Clock clock)
        {
            Blackboard blackboard = ReferencePool.Acquire<Blackboard>();
            blackboard.parentBlackboard = parent;
            blackboard.clock = clock;

            return blackboard;
        }

        public void Clear()
        {
            if (this.parentBlackboard != null)
            {
                this.parentBlackboard.children.Remove(this);
            }
            if (this.clock != null)
            {
                this.clock.RemoveTimer(this.NotifiyObserversActionCache);
            }
            data.Clear();
            observers.Clear();
            isNotifiyng = false;
            addObservers.Clear();
            removeObservers.Clear();
            notifications.Clear();
            notificationsDispatch.Clear();
            children.Clear();
        }

        public void Enable()
        {
            if (this.parentBlackboard != null)
            {
                this.parentBlackboard.children.Add(this);
            }
        }

        public void Disable()
        {
            if (this.parentBlackboard != null)
            {
                this.parentBlackboard.children.Remove(this);
            }
            if (this.clock != null)
            {
                this.clock.RemoveTimer(this.NotifiyObserversActionCache);
            }
        }



        public object this[string key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                Set(key, (Variable)value);
            }
        }

        public void Set(string key)
        {
            if (!Isset(key))
            {
                Set(key, null);
            }
        }

        public void Set<T>(string key, T value) where T : Variable
        {
            Set(key,(Variable)value);
        }

        public void Set(string key, Variable value)
        {
            

            if (this.parentBlackboard != null && this.parentBlackboard.Isset(key))
            {
                this.parentBlackboard.Set(key, value);
            }
            else
            {
                if (!this.data.ContainsKey(key))
                {
                    Variable oldData = Get(key);
                    if (oldData != null)
                    {
                        ReferencePool.Release(oldData);
                    }

                    this.data[key] = value;
                    this.notifications.Add(new Notification(key, Type.ADD, value));
                    this.clock.AddTimer(0f, 0, NotifiyObserversActionCache);
                }
                else
                {
                    if ((this.data[key] == null && value != null) || (this.data[key] != null && !this.data[key].Equals(value)))
                    {
                        Variable oldData = Get(key);
                        if (oldData != null)
                        {
                            ReferencePool.Release(oldData);
                        }

                        this.data[key] = value;
                        this.notifications.Add(new Notification(key, Type.CHANGE, value));                      
                        this.clock.AddTimer(0f, 0, NotifiyObserversActionCache);

                    }
                }
            }
        }

        public void Unset(string key)
        {
            if (this.data.ContainsKey(key))
            {
                this.data.Remove(key);
                this.notifications.Add(new Notification(key, Type.REMOVE, null));
                this.clock.AddTimer(0f, 0, NotifiyObserversActionCache);
            }
        }



        #region 旧代码

        //[System.Obsolete("Use Get<T> instead")]
        //public bool GetBool(string key)
        //{
        //    return Get<bool>(key);
        //}

        //[System.Obsolete("Use Get<T> instead - WARNING: return value for non-existant key will be 0.0f instead of float.NaN")]
        //public float GetFloat(string key)
        //{
        //    Variable result = Get(key);
        //    if (result == null)
        //    {
        //        return float.NaN;
        //    }
        //    return (float)Get(key);
        //}

        //[System.Obsolete("Use Get<T> instead")]
        //public Vector3 GetVector3(string key)
        //{
        //    return Get<Vector3>(key);
        //}

        //[System.Obsolete("Use Get<T> instead")]
        //public int GetInt(string key)
        //{
        //    return Get<int>(key);
        //}

        #endregion

        public T Get<T>(string key)
        {
            Variable result = Get(key);
            if (result == null)
            {
                return default(T);
            }

            Variable<T> finalResult = result as Variable<T>;

            if (finalResult == null)
            {
                throw new MDDGameFrameworkException("获取不到黑板值");
            }
            else
            {
                return finalResult.Value;
            }

            
        }

        public Variable Get(string key)
        {
            if (this.data.ContainsKey(key))
            {
                return data[key];
            }
            else if (this.parentBlackboard != null)
            {
                return this.parentBlackboard.Get(key);
            }
            else
            {
                return null;
            }
        }

        public bool Isset(string key)
        {
            return this.data.ContainsKey(key) || (this.parentBlackboard != null && this.parentBlackboard.Isset(key));
        }

        public void AddObserver(string key, System.Action<Type, Variable> observer)
        {
            List<System.Action<Type, Variable>> observers = GetObserverList(this.observers, key);
            if (!isNotifiyng)
            {
                if (!observers.Contains(observer))
                {
                    observers.Add(observer);
                }
            }
            else
            {
                if (!observers.Contains(observer))
                {
                    List<System.Action<Type, Variable>> addObservers = GetObserverList(this.addObservers, key);
                    if (!addObservers.Contains(observer))
                    {
                        addObservers.Add(observer);
                    }
                }

                List<System.Action<Type, Variable>> removeObservers = GetObserverList(this.removeObservers, key);
                if (removeObservers.Contains(observer))
                {
                    removeObservers.Remove(observer);
                }
            }
        }

        public void RemoveObserver(string key, System.Action<Type, Variable> observer)
        {
            List<System.Action<Type, Variable>> observers = GetObserverList(this.observers, key);
            if (!isNotifiyng)
            {
                if (observers.Contains(observer))
                {
                    observers.Remove(observer);
                }
            }
            else
            {
                List<System.Action<Type, Variable>> removeObservers = GetObserverList(this.removeObservers, key);
                if (!removeObservers.Contains(observer))
                {
                    if (observers.Contains(observer))
                    {
                        removeObservers.Add(observer);
                    }
                }

                List<System.Action<Type, Variable>> addObservers = GetObserverList(this.addObservers, key);
                if (addObservers.Contains(observer))
                {
                    addObservers.Remove(observer);
                }
            }
        }


#if UNITY_EDITOR
        public List<string> Keys
        {
            get
            {
                if (this.parentBlackboard != null)
                {
                    List<string> keys = this.parentBlackboard.Keys;
                    keys.AddRange(data.Keys);
                    return keys;
                }
                else
                {
                    return new List<string>(data.Keys);
                }
            }
        }

        public int NumObservers
        {
            get
            {
                int count = 0;
                foreach (string key in observers.Keys)
                {
                    count += observers[key].Count;
                }
                return count;
            }
        }
#endif


        private void NotifiyObservers()
        {
            if (notifications.Count == 0)
            {
                return;
            }

            notificationsDispatch.Clear();
            notificationsDispatch.AddRange(notifications);
            foreach (Blackboard child in children)
            {
                child.notifications.AddRange(notifications);
                child.clock.AddTimer(0f, 0, child.NotifiyObserversActionCache);
            }
            notifications.Clear();

            isNotifiyng = true;
            foreach (Notification notification in notificationsDispatch)
            {
                if (!this.observers.ContainsKey(notification.key))
                {
                    //                Debug.Log("1 do not notify for key:" + notification.key + " value: " + notification.value);
                    continue;
                }

                List<System.Action<Type, Variable>> observers = GetObserverList(this.observers, notification.key);
                foreach (System.Action<Type, Variable> observer in observers)
                {
                    if (this.removeObservers.ContainsKey(notification.key) && this.removeObservers[notification.key].Contains(observer))
                    {
                        continue;
                    }
                    observer(notification.type, notification.value);
                }
            }

            foreach (string key in this.addObservers.Keys)
            {
                GetObserverList(this.observers, key).AddRange(this.addObservers[key]);
            }
            foreach (string key in this.removeObservers.Keys)
            {
                foreach (System.Action<Type, Variable> action in removeObservers[key])
                {
                    GetObserverList(this.observers, key).Remove(action);
                }
            }
            this.addObservers.Clear();
            this.removeObservers.Clear();

            isNotifiyng = false;
        }

        private List<System.Action<Type, Variable>> GetObserverList(Dictionary<string, List<System.Action<Type, Variable>>> target, string key)
        {
            List<System.Action<Type, Variable>> observers;
            if (target.ContainsKey(key))
            {
                observers = target[key];
            }
            else
            {
                observers = new List<System.Action<Type, Variable>>();
                target[key] = observers;
            }
            return observers;
        }

       
    }
}
