using System.Collections.Generic;
using MDDSkillEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public sealed class KealSkillSystem : SkillSystem<Player>
    {
        public void UpdateKealBuffQueue(BuffBase buff)
        {
            ISkillSystem skillSystem = Game.Skill.GetSkillSystem(1001);

            Blackboard blackboard = skillSystem.GetPubBlackboard();
            int length;
            length = blackboard.Get<VarQueue>("队列").Value.Count;

            if (length < 3)
            {
                blackboard.Get<VarQueue>("队列").Value.Enqueue(buff);
            }
            else
            {
                BuffBase a = blackboard.Get<VarQueue>("队列").Value.Dequeue() as BuffBase;
                IBuffSystem buffSystem = Game.Buff.GetBuffSystem(Owner.Id.ToString());
            }
        }
    }
}
