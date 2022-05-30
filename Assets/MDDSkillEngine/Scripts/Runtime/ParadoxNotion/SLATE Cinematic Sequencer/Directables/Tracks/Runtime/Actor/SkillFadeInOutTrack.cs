using MDDSkillEngine;

namespace Slate
{
    [Name("技能中断控制轨道")]
    [Description("控制技能在何时进入不可中断的状态")]
    [Icon("Assets/GUIPack-Clean&Minimalist/Sprites/Icons-Demo-Small/Icons-Small-White_PNG/Arm-Muscles-Icon.png")]
    [Attachable(typeof(ActorGroup))]
    public class SkillFadeInOutTrack : ActionTrack
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            name = "技能中断控制轨道";
        }

        public override SkillDataType SkillDataType
        {
            get { return SkillDataType.InOut; }
        }
    }
}