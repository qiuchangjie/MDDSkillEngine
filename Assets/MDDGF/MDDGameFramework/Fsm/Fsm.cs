﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework
{
    /// <summary>
    /// 有限状态机。
    /// </summary>
    /// <typeparam name="T">有限状态机持有者类型。</typeparam>
    internal sealed class Fsm<T> : FsmBase, IReference, IFsm<T> where T : class
    {
        private T m_Owner;
        private readonly Dictionary<Type, FsmState<T>> m_States;
        private Dictionary<string, Variable> m_Datas;
        private Stack<FsmState<T>> m_StateStack;
        private FsmState<T> m_ButtonState;
        private FsmState<T> m_CurrentState;
        private float m_CurrentStateTime;
        private bool m_IsDestroyed;
        private Clock m_Clock;
        private Blackboard m_Blackboard;
        private float m_PlayableSpeed = 1f;

        /// <summary>
        /// 初始化有限状态机的新实例。
        /// </summary>
        public Fsm()
        {
            m_Owner = null;
            m_States = new Dictionary<Type, FsmState<T>>();
            m_Datas = null;
            m_CurrentState = null;
            m_CurrentStateTime = 0f;
            m_IsDestroyed = true;
            m_StateStack = new Stack<FsmState<T>>();
            m_Clock = new Clock();
            m_Blackboard = Blackboard.Create(m_Blackboard, m_Clock);
        }

        /// <summary>
        /// 获取有限状态机持有者。
        /// </summary>
        public T Owner
        {
            get
            {
                return m_Owner;
            }
        }

        /// <summary>
        /// 获取时间缩放速度
        /// </summary>
        public float PlayableSpeed
        {
            get { return m_PlayableSpeed; }
            set { m_PlayableSpeed = value; }
        }

        /// <summary>
        /// 获取有限状态机持有者类型。
        /// </summary>
        public override Type OwnerType
        {
            get
            {
                return typeof(T);
            }
        }

        /// <summary>
        /// 获取有限状态机中状态的数量。
        /// </summary>
        public override int FsmStateCount
        {
            get
            {
                return m_States.Count;
            }
        }

        /// <summary>
        /// 获取有限状态机是否正在运行。
        /// </summary>
        public override bool IsRunning
        {
            get
            {
                return m_CurrentState != null;
            }
        }

        /// <summary>
        /// 获取有限状态机是否被销毁。
        /// </summary>
        public override bool IsDestroyed
        {
            get
            {
                return m_IsDestroyed;
            }
        }

        /// <summary>
        /// 获取当前有限状态机状态。
        /// </summary>
        public FsmState<T> CurrentState
        {
            get
            {
                return m_StateStack.Peek();
            }
        }

        public FsmState<T> ButtomState
        {
            get 
            {
                return m_ButtonState; 
            }
        }

        /// <summary>
        /// 获取当前有限状态机状态名称。
        /// </summary>
        public override string CurrentStateName
        {
            get
            {
                return m_CurrentState != null ? m_CurrentState.GetType().FullName : null;
            }
        }

        /// <summary>
        /// 获取当前有限状态机状态持续时间。
        /// </summary>
        public override float CurrentStateTime
        {
            get
            {
                return m_CurrentStateTime;
            }
        }

        /// <summary>
        /// 获取当前状态机的计时器
        /// </summary>
        public Clock Clock
        {
            get { return m_Clock; }
        }

        /// <summary>
        /// 获取当前状态机的黑板
        /// </summary>
        public Blackboard Blackboard
        {
            get { return m_Blackboard; }    
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="name">有限状态机名称。</param>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>创建的有限状态机。</returns>
        public static Fsm<T> Create(string name, T owner, params FsmState<T>[] states)
        {
            if (owner == null)
            {
                throw new MDDGameFrameworkException("FSM owner is invalid.");
            }

            if (states == null || states.Length < 1)
            {
                throw new MDDGameFrameworkException("FSM states is invalid.");
            }

            Fsm<T> fsm = ReferencePool.Acquire<Fsm<T>>();
            fsm.Name = name;
            fsm.m_Owner = owner;
            fsm.m_IsDestroyed = false;
            fsm.m_StateStack = new Stack<FsmState<T>>();
            fsm.m_Clock = new Clock();
            fsm.m_Blackboard = Blackboard.Create(fsm.m_Blackboard, fsm.m_Clock);
            foreach (FsmState<T> state in states)
            {
                if (state == null)
                {
                    throw new MDDGameFrameworkException("FSM states is invalid.");
                }

              
                Type stateType = state.GetType();
                if (fsm.m_States.ContainsKey(stateType))
                {
                    throw new MDDGameFrameworkException(Utility.Text.Format("FSM '{0}' state '{1}' is already exist.", new TypeNamePair(typeof(T), name), stateType.FullName));
                }

                fsm.m_States.Add(stateType, state);
                state.OnInit(fsm);

                if (fsm.m_ButtonState == null)
                {
                    if (state.IsButtomState)
                    {
                        fsm.m_ButtonState = state;
                        fsm.m_CurrentStateTime = 0f;
                        fsm.m_StateStack.Push(state);
                    }
                }
            }

            return fsm;
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <param name="name">有限状态机名称。</param>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>创建的有限状态机。</returns>
        public static Fsm<T> Create(string name, T owner, List<FsmState<T>> states)
        {
            if (owner == null)
            {
                throw new MDDGameFrameworkException("FSM owner is invalid.");
            }

            if (states == null || states.Count < 1)
            {
                throw new MDDGameFrameworkException("FSM states is invalid.");
            }

            Fsm<T> fsm = ReferencePool.Acquire<Fsm<T>>();
            fsm.Name = name;
            fsm.m_Owner = owner;
            fsm.m_IsDestroyed = false;
            fsm.m_StateStack = new Stack<FsmState<T>>();
            fsm.m_Clock = new Clock();
            fsm.m_Blackboard = Blackboard.Create(fsm.m_Blackboard, fsm.m_Clock);
            foreach (FsmState<T> state in states)
            {
                if (state == null)
                {
                    throw new MDDGameFrameworkException("FSM states is invalid.");
                }

                Type stateType = state.GetType();
                if (fsm.m_States.ContainsKey(stateType))
                {
                    throw new MDDGameFrameworkException(Utility.Text.Format("FSM '{0}' state '{1}' is already exist.", new TypeNamePair(typeof(T), name), stateType.FullName));
                }

                fsm.m_States.Add(stateType, state);
                state.OnInit(fsm);

                if (fsm.m_ButtonState == null)
                {
                    if (state.IsButtomState)
                    {
                        fsm.m_ButtonState = state;
                        fsm.m_CurrentStateTime = 0f;
                        fsm.m_StateStack.Push(state);
                    }
                }
            }



            return fsm;
        }

        /// <summary>
        /// 清理有限状态机。
        /// </summary>
        public void Clear()
        {
            if (m_CurrentState != null)
            {
                m_CurrentState.OnLeave(this, true);
            }

            foreach (KeyValuePair<Type, FsmState<T>> state in m_States)
            {
                state.Value.OnDestroy(this);
            }

            Name = null;
            m_Owner = null;
            m_States.Clear();
            m_ButtonState = null;

            if (m_Datas != null)
            {
                foreach (KeyValuePair<string, Variable> data in m_Datas)
                {
                    if (data.Value == null)
                    {
                        continue;
                    }

                    ReferencePool.Release(data.Value);
                }

                m_Datas.Clear();
            }

            ReferencePool.Release(m_Blackboard);
            m_Blackboard = null;

            m_Clock = null;
            m_CurrentState = null;
            m_CurrentStateTime = 0f;
            m_IsDestroyed = true;
            m_PlayableSpeed = 1f;
        }

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <typeparam name="TState">要开始的有限状态机状态类型。</typeparam>
        public void Start<TState>() where TState : FsmState<T>
        {
            if (IsRunning)
            {
                throw new MDDGameFrameworkException("FSM is running, can not start again.");
            }

            FsmState<T> state = GetState<TState>();
            if (state == null)
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("FSM '{0}' can not start state '{1}' which is not exist.", new TypeNamePair(typeof(T), Name), typeof(TState).FullName));
            }

            if (state == m_ButtonState)
            {
                m_CurrentState = state;
                m_CurrentState.OnEnter(this);
                
            }
            else
            {
                m_CurrentStateTime = 0f;
                m_CurrentState = state;
                m_StateStack.Push(state);
                m_CurrentState.OnEnter(this);
            }           
        }

        /// <summary>
        /// 开始有限状态机。
        /// </summary>
        /// <param name="stateType">要开始的有限状态机状态类型。</param>
        public void Start(Type stateType)
        {
            if (IsRunning)
            {
                throw new MDDGameFrameworkException("FSM is running, can not start again.");
            }

            if (stateType == null)
            {
                throw new MDDGameFrameworkException("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            FsmState<T> state = GetState(stateType);
            if (state == null)
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("FSM '{0}' can not start state '{1}' which is not exist.", new TypeNamePair(typeof(T), Name), stateType.FullName));
            }

            if (state == m_ButtonState)
            {
                m_CurrentState = state;
                m_CurrentState.OnEnter(this);
            }
            else
            {
                m_CurrentStateTime = 0f;
                m_CurrentState = state;
                m_StateStack.Push(state);
                m_CurrentState.OnEnter(this);
            }
        }

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要检查的有限状态机状态类型。</typeparam>
        /// <returns>是否存在有限状态机状态。</returns>
        public bool HasState<TState>() where TState : FsmState<T>
        {
            return m_States.ContainsKey(typeof(TState));
        }

        /// <summary>
        /// 是否存在有限状态机状态。
        /// </summary>
        /// <param name="stateType">要检查的有限状态机状态类型。</param>
        /// <returns>是否存在有限状态机状态。</returns>
        public bool HasState(Type stateType)
        {
            if (stateType == null)
            {
                throw new MDDGameFrameworkException("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            return m_States.ContainsKey(stateType);
        }

        public string GetCurrStateName()
        {
            return CurrentState.GetType().Name;
        }

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要获取的有限状态机状态类型。</typeparam>
        /// <returns>要获取的有限状态机状态。</returns>
        public TState GetState<TState>() where TState : FsmState<T>
        {
            FsmState<T> state = null;
            if (m_States.TryGetValue(typeof(TState), out state))
            {
                return (TState)state;
            }

            return null;
        }

        /// <summary>
        /// 获取有限状态机状态。
        /// </summary>
        /// <param name="stateType">要获取的有限状态机状态类型。</param>
        /// <returns>要获取的有限状态机状态。</returns>
        public FsmState<T> GetState(Type stateType)
        {
            if (stateType == null)
            {
                throw new MDDGameFrameworkException("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(stateType))
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("State type '{0}' is invalid.", stateType.FullName));
            }

            FsmState<T> state = null;
            if (m_States.TryGetValue(stateType, out state))
            {
                return state;
            }

            return null;
        }

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <returns>有限状态机的所有状态。</returns>
        public FsmState<T>[] GetAllStates()
        {
            int index = 0;
            FsmState<T>[] results = new FsmState<T>[m_States.Count];
            foreach (KeyValuePair<Type, FsmState<T>> state in m_States)
            {
                results[index++] = state.Value;
            }

            return results;
        }

        /// <summary>
        /// 获取有限状态机的所有状态。
        /// </summary>
        /// <param name="results">有限状态机的所有状态。</param>
        public void GetAllStates(List<FsmState<T>> results)
        {
            if (results == null)
            {
                throw new MDDGameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<Type, FsmState<T>> state in m_States)
            {
                results.Add(state.Value);
            }
        }

        /// <summary>
        /// 是否存在有限状态机数据。
        /// </summary>
        /// <param name="name">有限状态机数据名称。</param>
        /// <returns>有限状态机数据是否存在。</returns>
        public bool HasData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new MDDGameFrameworkException("Data name is invalid.");
            }

            if (m_Blackboard == null)
            {
                return false;
            }

            return m_Blackboard.Isset(name);
        }

        /// <summary>
        /// 获取有限状态机数据。
        /// </summary>
        /// <typeparam name="TData">要获取的有限状态机数据的类型。</typeparam>
        /// <param name="name">有限状态机数据名称。</param>
        /// <returns>要获取的有限状态机数据。</returns>
        public TData GetData<TData>(string name) where TData : Variable
        {
            return (TData)GetData(name);
        }

        /// <summary>
        /// 获取有限状态机数据。
        /// </summary>
        /// <param name="name">有限状态机数据名称。</param>
        /// <returns>要获取的有限状态机数据。</returns>
        public Variable GetData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new MDDGameFrameworkException("Data name is invalid.");
            }

            if (m_Blackboard == null)
            {
                return null;
            }

            return m_Blackboard.Get(name);
        }

        /// <summary>
        /// 设置有限状态机数据。
        /// </summary>
        /// <typeparam name="TData">要设置的有限状态机数据的类型。</typeparam>
        /// <param name="name">有限状态机数据名称。</param>
        /// <param name="data">要设置的有限状态机数据。</param>
        public void SetData<TData>(string name, TData data) where TData : Variable
        {
            if (m_Blackboard == null)
            {
                throw new MDDGameFrameworkException("m_Blackboard is invalid.");
            }

            m_Blackboard.Set(name, data);
        }

        /// <summary>
        /// 设置有限状态机数据。
        /// </summary>
        /// <param name="name">有限状态机数据名称。</param>
        /// <param name="data">要设置的有限状态机数据。</param>
        public void SetData(string name, Variable data)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new MDDGameFrameworkException("Data name is invalid.");
            }

            if (m_Blackboard == null)
            {
                throw new MDDGameFrameworkException("m_Blackboard is invalid.");
            }

            m_Blackboard.Set(name, data);
        }

        /// <summary>
        /// 移除有限状态机数据。
        /// </summary>
        /// <param name="name">有限状态机数据名称。</param>
        /// <returns>是否移除有限状态机数据成功。</returns>
        public void RemoveData(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new MDDGameFrameworkException("Data name is invalid.");
            }

            if (m_Blackboard == null)
            {
                throw new MDDGameFrameworkException("m_Blackboard is invalid.");
            }

            m_Blackboard.Unset(name);
        }

        /// <summary>
        /// 有限状态机轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_CurrentState == null)
            {
                return;
            }

            m_CurrentStateTime += elapseSeconds;
            m_StateStack.Peek().OnUpdate(this, elapseSeconds, realElapseSeconds);

            m_Clock.Update(elapseSeconds);
        }

        /// <summary>
        /// 有限状态机物理轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal override void FixedUpdate(float elapseSeconds, float realElapseSeconds)
        {
            if (m_CurrentState == null)
            {
                return;
            }

            m_CurrentStateTime += elapseSeconds;
            m_StateStack.Peek().OnFixedUpdate(this, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 关闭并清理有限状态机。
        /// </summary>
        internal override void Shutdown()
        {
            ReferencePool.Release(this);
        }

        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <typeparam name="TState">要切换到的有限状态机状态类型。</typeparam>
        internal void ChangeState<TState>() where TState : FsmState<T>
        {
            ChangeState(typeof(TState));
        }

        /// <summary>
        /// 结束当前状态的生命周期
        /// </summary>
        /// <exception cref="MDDGameFrameworkException"></exception>
        internal void FinishState()
        {
            if (m_CurrentState == null)
            {
                throw new MDDGameFrameworkException("Current state is invalid.");
            }

            //栈底状态无法结束生命周
            if (!m_StateStack.Peek().IsButtomState)
            {
                m_StateStack.Peek().OnLeave(this, false);
                m_StateStack.Pop();
                m_StateStack.Peek().OnEnter(this);
            }      
        }


        /// <summary>
        /// 切换当前有限状态机状态。
        /// </summary>
        /// <param name="stateType">要切换到的有限状态机状态类型。</param>
        internal void ChangeState(Type stateType)
        {         
            if (m_CurrentState == null)
            {
                throw new MDDGameFrameworkException("Current state is invalid.");
            }

            if (m_CurrentState.CantStop)
            {
                return;
            }

            FsmState<T> state = GetState(stateType);
            if (state == null)
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("FSM '{0}' can not change state to '{1}' which is not exist.", new TypeNamePair(typeof(T), Name), stateType.FullName));
            }

            //如果状态无法中断则返回操作
            if (m_StateStack.Peek().CantStop)
            {
                return;
            }

            //如果为栈底状态则清空其他所有状态直接进入栈底状态
            if (state.IsButtomState)
            {
                foreach (var item in m_StateStack)
                {
                    if (!item.IsButtomState)
                    {
                        item.OnLeave(this, false);
                    }
                }
                m_StateStack.Clear();
                m_StateStack.Push(state);
                m_CurrentStateTime = 0f;
                m_StateStack.Peek().OnEnter(this);
                return;
            }

            //如果为可保存状态则正常切换
            if (!m_StateStack.Peek().StrongState && !m_StateStack.Peek().IsButtomState)
            {               
                m_StateStack.Peek().OnLeave(this, false);
                m_CurrentStateTime = 0f;
                m_StateStack.Pop();
                m_StateStack.Push(state);
                m_StateStack.Peek().OnEnter(this);
            }
            else//如果为可保存状态则保存状态不出栈
            {
                m_StateStack.Push(state);
                m_StateStack.Peek().OnEnter(this);
            }            
        }

        public void AddObserver(string key, System.Action<Blackboard.Type, Variable> action)
        {
            Blackboard.AddObserver(key, action);
        }
    }
}
