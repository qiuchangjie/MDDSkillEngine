using MDDSkillEngine;

namespace Slate
{
    [Name("CD轨道")]
    [Description("控制技能在行进何时进入cd，同时也是判定技能是否释放成功的时间点")]
    [Icon("Assets/GUIPack-Clean&Minimalist/Sprites/Icons-Demo-Small/Icons-Small-White_PNG/Hourglass-Icon.png")]
    [Attachable(typeof(ActorGroup))]
    public class CDTrack : ActionTrack
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            name = "CD轨道";
        }

        public override SkillDataType SkillDataType
        {
            get { return SkillDataType.CD; }
        }
    }
}