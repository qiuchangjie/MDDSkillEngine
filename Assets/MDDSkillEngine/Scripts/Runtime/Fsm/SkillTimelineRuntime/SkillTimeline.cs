﻿using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    /// <summary>
    /// 技能运行时辅助类
    /// 主要用于将slate逻辑复制过来
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SkillTimeline <T> where T : class
    {
        public enum PlayingDirection
        {
            Forwards,
            Backwards
        }

        public List<SkillClip> skillClips = new List<SkillClip>();

        private float currentTime;

        private float lastTime;

        private float length;

        private float previousTime;

        public PlayingDirection playingDirection = PlayingDirection.Forwards;

        public float playbackSpeed;

        private List<IDirectableTimePointer> timePointers;
        private List<IDirectableTimePointer> unsortedStartTimePointers;


        public void Updata(float delta)
        {
            delta *= playbackSpeed;
            currentTime += playingDirection == PlayingDirection.Forwards ? delta : -delta;

            Sample(currentTime);
        }

        public void InitTimePointer()
        {
            foreach (var item in skillClips)
            {

            }
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
        public float duration;

        public float startTime;

        public float endTime;


        abstract public void Enter();
        abstract public void Exit();
        virtual public void Update(float currentTime, float previousTime)
        {

        }
    }


    ///An interface for TimePointers (since structs can't be abstract)
    public interface IDirectableTimePointer
    {
        SkillClip target { get; }
        float time { get; }
        void TriggerForward(float currentTime, float previousTime);
        void TriggerBackward(float currentTime, float previousTime);
        void Update(float currentTime, float previousTime);
    }

    ///----------------------------------------------------------------------------------------------

    ///Wraps the startTime of a group, track or clip (IDirectable) along with it's relevant execution
    public class StartTimePointer : IDirectableTimePointer
    {

        private bool triggered;
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
    public class EndTimePointer : IDirectableTimePointer
    {

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





