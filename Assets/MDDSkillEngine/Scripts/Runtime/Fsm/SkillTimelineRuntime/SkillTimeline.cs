using MDDGameFramework.Runtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MDDSkillEngine
{
    public abstract class SkillTimeline
    {
        
    }

    /// <summary>
    /// 技能运行时辅助类
    /// 主要用于将slate逻辑复制过来
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SkillTimeline<T> : SkillTimeline where T : Entity
    {
        public enum PlayingDirection
        {
            Forwards,
            Backwards
        }

        public T owner;

        public List<SkillClip> skillClips = new List<SkillClip>();

        public float currentTime;

        public float lastTime;

        public float length;

        public float previousTime;

        public PlayingDirection playingDirection = PlayingDirection.Forwards;

        public float playbackSpeed = 1f;

        private List<IDirectableTimePointer> timePointers = new List<IDirectableTimePointer>();
        private List<IDirectableTimePointer> unsortedStartTimePointers = new List<IDirectableTimePointer>();


        public void Updata(float delta)
        {
            delta *= playbackSpeed;
            currentTime += playingDirection == PlayingDirection.Forwards ? delta : -delta;

            Sample(currentTime);
        }

        public void Exit()
        {
            foreach (var v in timePointers)
            {
                v.isTrigger = false;
            }

            foreach (var v in unsortedStartTimePointers)
            {
                v.isTrigger = false;
            }
        }
      
        /// <summary>
        /// 初始化技能clip
        /// </summary>
        /// <param name="skillData"></param>
        public void InitSkillClip(SkillData skillData)
        {
            foreach (var data in skillData.skillData)
            {
                switch (data.DataType)
                {
                    case SkillDataType.Effect:
                        {
                            EffectClip effectClip = new EffectClip();
                            effectClip.Init(data, owner,this);
                            skillClips.Add(effectClip);
                        }
                        break;
                    case SkillDataType.Animation:
                        {
                            AnimationClip animationClip = new AnimationClip();
                            animationClip.Init(data, owner, this);
                            skillClips.Add(animationClip);
                        }
                        break;
                    case SkillDataType.Collider:
                        {
                            ColliderClip colliderClip = new ColliderClip();
                            colliderClip.Init(data, owner, this);
                            skillClips.Add(colliderClip);
                        }
                        break;

                }
            }

            length = skillData.Length;

            InitTimePointer();

            Log.Info("{0}初始化Skilltimeline:name:{1} 成功.", LogConst.SKillTimeline, owner.Name);
        }

        /// <summary>
        /// 初始化时间点
        /// </summary>
        private void InitTimePointer()
        {
            foreach (var item in skillClips)
            {
                var timePointer = new StartTimePointer(item);

                timePointers.Add(timePointer);
                timePointers.Add(new EndTimePointer(item));

                unsortedStartTimePointers.Add(timePointer);                
            }

            timePointers = timePointers.OrderBy(p => p.time).ToList();
        }

        private void Sample(float time)
        {
            currentTime = time;

            //ignore same minmax times
            if ((currentTime == 0 || currentTime == length) && previousTime == currentTime)
            {
                return;
            }

            //Sample pointers
            if (timePointers != null)
            {
                //Log.Error("Sample pointers");
                Internal_SamplePointers(currentTime, previousTime);
            }


            previousTime = currentTime;
        }

        private void Internal_SamplePointers(float currentTime, float previousTime)
        {
            if (!Application.isPlaying || currentTime > previousTime)
            {
                for (var i = 0; i < timePointers.Count; i++)
                {
                    try
                    {
                        //Debug.LogError("TriggerForward");
                        timePointers[i].TriggerForward(currentTime, previousTime);
                    }
                    catch (System.Exception e) { Debug.LogException(e); }
                }
            }

            //Update timePointers triggering backwards
            if (!Application.isPlaying || currentTime < previousTime)
            {
                for (var i = timePointers.Count - 1; i >= 0; i--)
                {
                    try { timePointers[i].TriggerBackward(currentTime, previousTime); }
                    catch (System.Exception e) { Debug.LogException(e); }
                }
            }

            //Update timePointers
            if (unsortedStartTimePointers != null)
            {
                for (var i = 0; i < unsortedStartTimePointers.Count; i++)
                {
                    try { unsortedStartTimePointers[i].Update(currentTime, previousTime); }
                    catch (System.Exception e) { Debug.LogException(e); }
                }
            }
        }
    }


    public abstract class SkillClip
    {
        public Entity actor;

        public SkillTimeline skillTimeline;

        public float duration;

        public float startTime;

        public float endTime;

        virtual public void Init(SkillDataBase data, Entity actor, SkillTimeline skillTimeline)
        {
            startTime = data.StartTime;
            endTime = data.EndTime;
        }

        abstract public void Enter();
        abstract public void Exit();
        virtual public void Update(float currentTime, float previousTime)
        {

        }
    }


    ///An interface for TimePointers (since structs can't be abstract)
    public interface IDirectableTimePointer
    {
        bool isTrigger { get; set; }
        SkillClip target { get; }
        float time { get; }
        void TriggerForward(float currentTime, float previousTime);
        void TriggerBackward(float currentTime, float previousTime);
        void Update(float currentTime, float previousTime);
    }

    ///----------------------------------------------------------------------------------------------

    ///Wraps the startTime of a group, track or clip (IDirectable) along with it's relevant execution
    public struct StartTimePointer : IDirectableTimePointer
    {
        public bool isTrigger 
        {
            get
            {
                return triggered;
            }
            set
            {
                triggered = value;
            }
        }

        public bool triggered;
        private float lastTargetStartTime;
        public SkillClip target { get; private set; }
        float IDirectableTimePointer.time { get { return target.startTime; } }

        public StartTimePointer(SkillClip target)
        {
            this.target = target;
            triggered = false;
            lastTargetStartTime = target.startTime;
        }

        //...
        void IDirectableTimePointer.TriggerForward(float currentTime, float previousTime)
        {
            if (currentTime >= target.startTime)
            {
                if (!triggered)
                {
                    //Debug.LogError($"triggered{target.name}");
                    triggered = true;
                    target.Enter();
                    target.Update(target.ToLocalTime(currentTime), 0);
                }
            }
        }

        //...
        void IDirectableTimePointer.Update(float currentTime, float previousTime)
        {

            //update target and try auto-key
            if (currentTime >= target.startTime && currentTime < target.endTime && currentTime > 0)
            {

                var deltaMoveClip = target.startTime - lastTargetStartTime;
                var localCurrentTime = target.ToLocalTime(currentTime);
                var localPreviousTime = target.ToLocalTime(previousTime + deltaMoveClip);


                target.Update(localCurrentTime, localPreviousTime);
                lastTargetStartTime = target.startTime;
            }
        }

        //...
        void IDirectableTimePointer.TriggerBackward(float currentTime, float previousTime)
        {
            if (currentTime < target.startTime || currentTime <= 0)
            {
                if (triggered)
                {
                    triggered = false;
                    target.Update(0, target.ToLocalTime(previousTime));
                    //target.Reverse();
                }
            }
        }
    }

    ///----------------------------------------------------------------------------------------------

    ///Wraps the endTime of a group, track or clip (IDirectable) along with it's relevant execution
    public struct EndTimePointer : IDirectableTimePointer
    {
        public bool isTrigger
        {
            get
            {
                return triggered;
            }
            set
            {
                triggered = value;
            }
        }

        private bool triggered;
        public SkillClip target { get; private set; }
        float IDirectableTimePointer.time { get { return target.endTime; } }

        public EndTimePointer(SkillClip target)
        {
            this.target = target;
            triggered = false;
        }

        //...
        void IDirectableTimePointer.TriggerForward(float currentTime, float previousTime)
        {
            if (currentTime >= target.endTime)
            {
                if (!triggered)
                {
                    triggered = true;
                    target.Update(target.GetLength(), target.ToLocalTime(previousTime));
                    target.Exit();
                }
            }
        }

        //...
        void IDirectableTimePointer.Update(float currentTime, float previousTime)
        {
            //Update is/should never be called in TimeOutPointers
            throw new System.NotImplementedException();
        }

        //...
        void IDirectableTimePointer.TriggerBackward(float currentTime, float previousTime)
        {
            if (currentTime < target.endTime)
            {
                if (triggered)
                {
                    triggered = false;
                    //target.ReverseEnter();
                    target.Update(target.ToLocalTime(currentTime), target.GetLength());
                }
            }
        }
    }
}






