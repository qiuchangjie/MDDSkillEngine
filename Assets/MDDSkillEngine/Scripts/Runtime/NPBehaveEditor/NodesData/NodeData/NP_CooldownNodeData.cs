using Sirenix.OdinInspector;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    [HideLabel]
    public class NP_CooldownNodeData : NP_NodeDataBase
    {
        Cooldown m_Cooldown;

        [BoxGroup("冷却时间")]
        [LabelText("冷却时间")]
        public float cooldownTime = 0;

        public override Decorator CreateDecoratorNode(object m_object, NP_Tree runtimeTree, Node node)
        {
            this.m_Cooldown = Cooldown.Create(cooldownTime, node);
            return this.m_Cooldown;
        }

        public override Node NP_GetNode()
        {
            return this.m_Cooldown;
        }
    }
}


