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

        public IEnumerator StartJobDelayed(float delayedTime)
        {
            yield return new WaitForSeconds(delayedTime);
        }

        public IEnumerator StopForSecondsUnScale(float time)
        {
            Time.timeScale = 0;
            yield return YieldHelper.WaitForSeconds(time, true);
            Time.timeScale = 1;
        }

        public IEnumerator StopFixedFrame(int num = 1)
        {
            Time.timeScale = 0;
            for (int i = 0; i < num; i++)
            {
                yield return YieldHelper.WaitForSeconds(0.02f, true);
            }
            Time.timeScale = 1;
        }

        public IEnumerator StartJobDelayed<T>(float delayedTime, T t)
        {
            yield return new WaitForSeconds(delayedTime);
        }

        public IEnumerator StartJobUntil(Func<bool> funtion)
        {
            yield return new WaitUntil(funtion);
        }

        public IEnumerator StartJobUntil<T>(Func<bool> funtion, T t)
        {
            yield return new WaitUntil(funtion);
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

        public static CoroutineHandler Start(this IEnumerator enumerator, Action<bool> Callback)
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
            //ReferencePool.Release(this);
        }

        public void Clear()
        {
            this.CompletedAction = null;
            this.Coroutine = null;
        }

        /// <summary>
        /// 用于在untiy自带协程上再进行一次封装
        /// 给unity自带协程扩展一些生命周期
        /// </summary>
        /// <returns></returns>
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


