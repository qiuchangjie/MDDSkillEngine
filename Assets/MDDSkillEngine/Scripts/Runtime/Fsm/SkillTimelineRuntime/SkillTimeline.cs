using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    abstract public void Enter();
    abstract public void Exit();
    abstract public void Update();  
}




