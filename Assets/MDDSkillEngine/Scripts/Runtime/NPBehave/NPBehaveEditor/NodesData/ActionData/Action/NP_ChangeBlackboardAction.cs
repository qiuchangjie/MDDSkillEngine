using Sirenix.OdinInspector;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    [Title("修改或比对黑板值", TitleAlignment = TitleAlignments.Centered)]
    public class NP_ChangeBlackboardAction : NP_ClassForAction
    {
        [LabelText("输入要改变的黑板值")]
        public ClassForBlackboard data = new ClassForBlackboard();

        public override System.Action GetActionToBeDone()
        {
            this.Action = this.ChangeValue;
            return this.Action;
        }

        public void ChangeValue()
        {
            Log.Info("更改黑板值:{0},{1}", data.BBKey, data.NP_BBValue);
            BelongtoRuntimeTree.Root.Blackboard.Set(data.BBKey, data.NP_BBValue.VDeepCopy());
        }
    }
}


