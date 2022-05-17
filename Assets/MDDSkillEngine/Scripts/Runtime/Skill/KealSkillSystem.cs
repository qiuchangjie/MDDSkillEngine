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

                KaerFire buf = buff as KaerFire;
                if (buf != null)
                {
                   float num =  blackboard.Get<float>("火");
                   num++;
                   blackboard.Set<VarFloat>("火",num);
                }
                KaerIce buf1 = buff as KaerIce;
                if (buf1 != null)
                {
                    float num = blackboard.Get<float>("冰");
                    num++;
                    blackboard.Set<VarFloat>("冰", num);
                }
                KaerThunder buf2 = buff as KaerThunder;
                if (buf2 != null)
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

                KaerFire bufff = a as KaerFire;
                if (bufff != null)
                {
                    float num = blackboard.Get<float>("火");
                    num--;
                    blackboard.Set<VarFloat>("火", num);
                }
                KaerIce buff1 = a as KaerIce;
                if (buff1 != null)
                {
                    float num = blackboard.Get<float>("冰");
                    num--;
                    blackboard.Set<VarFloat>("冰", num);
                }
                KaerThunder buff2 = a as KaerThunder;
                if (buff2 != null)
                {
                    float num = blackboard.Get<float>("雷");
                    num--;
                    blackboard.Set<VarFloat>("雷", num);
                }

                buffQueue.Enqueue(buff);

                KaerFire buf = buff as KaerFire;
                if (buf != null)
                {
                    float num = blackboard.Get<float>("火");
                    num++;
                    blackboard.Set<VarFloat>("火", num);
                }
                KaerIce buf1 = buff as KaerIce;
                if (buf1 != null)
                {
                    float num = blackboard.Get<float>("冰");
                    num++;
                    blackboard.Set<VarFloat>("冰", num);
                }
                KaerThunder buf2 = buff as KaerThunder;
                if (buf2 != null)
                {
                    float num = blackboard.Get<float>("雷");
                    num++;
                    blackboard.Set<VarFloat>("雷", num);
                }
            }
        }
    }
}
