using MDDSkillEngine;

namespace Slate
{
    [Name("实体轨道")]
    [Description("可以用来负责生成召唤物")]
    [Icon("Assets/GUIPack-Clean&Minimalist/Sprites/Icons-Demo-Small/Icons-Small-White_PNG/Body-Icon.png")]
    [Attachable(typeof(ActorGroup))]
    public class EntityTrack : ActionTrack
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            name = "实体轨道";
        }

        public override SkillDataType SkillDataType
        {
            get { return SkillDataType.Entity; }
        }
    }
}