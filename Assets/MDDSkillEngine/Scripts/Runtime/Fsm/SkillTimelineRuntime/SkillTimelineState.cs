using MDDGameFramework;

namespace MDDSkillEngine
{
    public class SkillTimelineState<T> : FsmState<T> where T : class
    {
        public SkillTimeline<T> skillTimeline;

    }
}
