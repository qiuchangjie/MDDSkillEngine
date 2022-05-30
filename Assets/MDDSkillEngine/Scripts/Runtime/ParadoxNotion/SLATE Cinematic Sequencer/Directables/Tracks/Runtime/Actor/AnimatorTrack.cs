using MDDSkillEngine;

namespace Slate
{

    //Partial to easier manage different unity versions since they differ that much in this case
    public partial class AnimatorTrack : CutsceneTrack
    {
        protected override void OnCreate()
        {
            base.OnCreate();
            name = "动画轨道";
        }

        public override SkillDataType SkillDataType
        {
            get { return SkillDataType.Animation; }
        }
    }
}