using MDDSkillEngine;

namespace Slate
{
    [Name("结束状态轨道轨道")]
    [Description("判定该技能状态何时结束，\n" +
        "skilltimeline 在runtime中使用协程轮询所以结束状态不会影响timeline的运行")]
    [Icon("Assets/GUIPack-Clean&Minimalist/Sprites/Icons-Demo-Small/Icons-Small-White_PNG/Hourglass-Icon.png")]
    [Attachable(typeof(ActorGroup))]
    public class FinishStateTrack : ActionTrack
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            name = "结束状态轨道轨道";
        }

        public override SkillDataType SkillDataType
        {
            get { return SkillDataType.FinishState; }
        }
    }
}