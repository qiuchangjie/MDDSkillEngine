using MDDSkillEngine;

namespace Slate
{
    [Name("buff轨道")]
    [Description("控制buff在何时添加，添加目标依据为技能target类型")]
    [Icon("Assets/GUIPack-Clean&Minimalist/Sprites/Icons-Demo-Small/Icons-Small-White_PNG/Book-Opened-Icon.png")]
    [Attachable(typeof(ActorGroup))]
    public class AddBuffTrack : ActionTrack 
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            name = "buff轨道";
        }

        public override SkillDataType SkillDataType
        {
            get { return SkillDataType.Buff; }
        }
    }
}