using MDDGameFramework;
using MDDGameFramework.Runtime;
using Sirenix.OdinInspector;

namespace MDDSkillEngine
{
    [Title("添加buff", TitleAlignment = TitleAlignments.Centered)]
    public class NP_AddBuffAction : NP_ClassForAction
    {

        public string BuffName;

        public override System.Action GetActionToBeDone()
        {
            this.Action = this.AddBuff;
            return this.Action;
        }

        public void AddBuff()
        {
            Entity entity = owner as Entity;
            if (entity == null)
            {
                Log.Error("试图给不是实体的物体添加buff{0}", owner.ToString());
            }

            Game.Buff.AddBuff(entity.Id.ToString(), BuffName, entity, entity);
        }
    }
}


