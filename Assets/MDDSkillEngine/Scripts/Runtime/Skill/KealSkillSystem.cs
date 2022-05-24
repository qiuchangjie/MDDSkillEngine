using MDDSkillEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;

namespace MDDSkillEngine
{
    public sealed class KealSkillSystem : SkillSystem<Player>
    {
        private Queue<BuffBase> buffQueue = new Queue<BuffBase>();

        public KealSkillSystem()
        {

        }

        public static KealSkillSystem Create(Entity Owner)
        {
            KealSkillSystem skillSystem = ReferencePool.Acquire<KealSkillSystem>();
            skillSystem.m_Owner = Owner as Player;
            return skillSystem;
        }

        public void UpdateKealBuffQueue(BuffBase buff)
        {
            ISkillSystem skillSystem = Game.Skill.GetSkillSystem(1001);

            Blackboard blackboard = skillSystem.GetPubBlackboard();
            int length;
            length = buffQueue.Count;

            if (length < 3)
            {
                buffQueue.Enqueue(buff);

                if (buff is KaerFire)
                {
                    float num = blackboard.Get<float>("火");
                    num++;
                    blackboard.Set<VarFloat>("火", num);
                }

                if (buff is KaerIce)
                {
                    float num = blackboard.Get<float>("冰");
                    num++;
                    blackboard.Set<VarFloat>("冰", num);
                }

                if (buff is KaerThunder)
                {
                    float num = blackboard.Get<float>("雷");
                    num++;
                    blackboard.Set<VarFloat>("雷", num);
                }

            }
            else
            {
                BuffBase a = buffQueue.Dequeue() as BuffBase;
                IBuffSystem buffSystem = Game.Buff.GetBuffSystem(Owner.Id.ToString());
                buffSystem.RemoveBuff(a);

                if (a is KaerFire)
                {
                    float num = blackboard.Get<float>("火");
                    num--;
                    blackboard.Set<VarFloat>("火", num);
                }

                if (a is KaerIce)
                {
                    float num = blackboard.Get<float>("冰");
                    num--;
                    blackboard.Set<VarFloat>("冰", num);
                }

                if (a is KaerThunder)
                {
                    float num = blackboard.Get<float>("雷");
                    num--;
                    blackboard.Set<VarFloat>("雷", num);
                }

                buffQueue.Enqueue(buff);

                if (buff is KaerFire)
                {
                    float num = blackboard.Get<float>("火");
                    num++;
                    blackboard.Set<VarFloat>("火", num);
                }

                if (buff is KaerIce)
                {
                    float num = blackboard.Get<float>("冰");
                    num++;
                    blackboard.Set<VarFloat>("冰", num);
                }

                if (buff is KaerThunder)
                {
                    float num = blackboard.Get<float>("雷");
                    num++;
                    blackboard.Set<VarFloat>("雷", num);
                }
            }
        }
    }
}
