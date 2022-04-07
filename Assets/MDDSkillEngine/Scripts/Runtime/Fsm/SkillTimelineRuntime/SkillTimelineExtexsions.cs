
using UnityEngine;

namespace MDDSkillEngine
{
    public static class SkillTimelineExtexsions
    {
        public static float GetLength(this SkillClip skillClip)
        {
            return skillClip.endTime - skillClip.startTime;
        }

        public static float ToLocalTime(this SkillClip skillClip, float time)
        {
            return Mathf.Clamp(time - skillClip.startTime, 0, skillClip.GetLength());
        }
    }
}
