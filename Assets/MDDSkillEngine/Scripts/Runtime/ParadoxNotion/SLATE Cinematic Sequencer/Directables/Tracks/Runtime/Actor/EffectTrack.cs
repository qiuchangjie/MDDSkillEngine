using MDDSkillEngine;

namespace Slate
{
    [Name("特效轨道")]
    [Description("特效轨道 目前只控制特效的生成以及生成坐标scale相关参数")]
    [Icon("Assets/GUIPack-Clean&Minimalist/Sprites/Icons-Demo-Small/Icons-Small-White_PNG/Cloud-Icon.png")]
    [Attachable(typeof(ActorGroup))]
    public class EffectTrack : ActionTrack 
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            name = "特效轨道";
        }

        public override SkillDataType SkillDataType
        {
            get { return SkillDataType.Effect; }
        }
    }
}