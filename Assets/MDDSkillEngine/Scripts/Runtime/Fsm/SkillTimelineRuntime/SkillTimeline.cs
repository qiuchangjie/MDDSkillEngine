using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine 
{
    public class SkillTimeline
    {
        public List<SkillClip> skillClips = new List<SkillClip>();

        private float lastTime;



    }


    public abstract class SkillClip
    {
        public float duration;

        public float startTime;

        public float endTime;

        private bool isTriggered;

        abstract public void Enter();
        abstract public void Exit();
        virtual public void Update()
        {

        }
    }
}






