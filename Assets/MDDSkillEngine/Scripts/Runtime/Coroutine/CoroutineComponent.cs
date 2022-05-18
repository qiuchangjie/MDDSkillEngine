using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using MDDGameFramework;

namespace MDDSkillEngine
{
    public class CoroutineComponent : MDDGameFrameworkComponent
    {
        public Coroutine Run(IEnumerator target)
        {
            return StartCoroutine(target);
        }
    }
    public static class CoroutineEx
    {
        public static CoroutineHandler Start(this IEnumerator enumerator)
        {
            CoroutineHandler handler = ReferencePool.Acquire<CoroutineHandler>();
            handler.Coroutine = enumerator;
            handler.Start();
            return handler;
        }

        public static CoroutineHandler Start(this IEnumerator enumerator,Action<bool> Callback)
        {
            CoroutineHandler handler = ReferencePool.Acquire<CoroutineHandler>();
            handler.Coroutine = enumerator;
            handler.CompletedAction = Callback;
            handler.Start();
            return handler;
        }
    }

    public class CoroutineHandler : IReference
    {
        public IEnumerator Coroutine = null;
        public bool Paused { get; private set; } = false;
        public bool Running { get; private set; } = false;
        public bool Stopped { get; private set; } = false;
      
        public Action<bool> CompletedAction;

        public CoroutineHandler()
        {
            
        }

        public CoroutineHandler(IEnumerator c)
        {
            Coroutine = c;
        }

        public void Pause()
        {
            Paused = true;
        }

        public void Resume()
        {
            Paused = false;
        }

        public void Start()
        {
            if (null != Coroutine)
            {
                Running = true;
                Game.Coroutine.Run(CallWrapper());
            }
            else
            {
                Log.Info("Coroutine 未指定，避免直接调用该方法。");
            }
        }

        public void Stop()
        {
            Stopped = true;
            Running = false;
        }

        /// <summary>
        /// 完成回调并断引用
        /// </summary>
        private void Finish()
        {
            CompletedAction?.Invoke(Stopped);
            ReferencePool.Release(this);
        }

        public void Clear()
        {
            this.CompletedAction = null;
            this.Coroutine = null;
        }
        
        IEnumerator CallWrapper()
        {
            yield return null;
            IEnumerator e = Coroutine;
            while (Running)
            {
                if (Paused)
                    yield return null;
                else
                {
                    if (e != null && e.MoveNext())
                    {
                        yield return e.Current;
                    }
                    else
                    {
                        Running = false;
                    }
                }
            }
            Finish();
        }
   
    }
}


