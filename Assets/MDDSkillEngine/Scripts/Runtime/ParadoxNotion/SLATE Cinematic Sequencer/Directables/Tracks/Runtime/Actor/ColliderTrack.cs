using MDDSkillEngine;

namespace Slate
{
    [Name("碰撞体轨道")]
    [Description("碰撞体轨道 目前只控制碰撞体的生成以及生成坐标scale相关参数")]
    [Icon("Assets/GUIPack-Clean&Minimalist/Sprites/Icons-Demo-Small/Icons-Small-White_PNG/Box-Outline-Icon.png")]
    [Attachable(typeof(ActorGroup))]
    public class ColliderTrack : ActionTrack 
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            name = "碰撞体轨道";
        }

        public override SkillDataType SkillDataType
        {
            get { return SkillDataType.Collider; }
        }
    }
}