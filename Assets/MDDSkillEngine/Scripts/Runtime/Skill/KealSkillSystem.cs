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
                buffSystem.
                    RemoveBuff(a);

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


        public override void ReleaseSkill(int id)
        {
            base.ReleaseSkill(id);
            if (id == 10019)
            {
                if (GetPubBlackboard().Get<float>("冰") == 1 && GetPubBlackboard().Get<float>("火") == 2)
                {
                    Game.Event.Fire(this, KaerQihuanEventArgs.Create(this, 10016));
                }
                else if (GetPubBlackboard().Get<float>("冰") == 1 && GetPubBlackboard().Get<float>("雷") == 2)
                {
                    Game.Event.Fire(this, KaerQihuanEventArgs.Create(this, 10011));
                }
                else if (GetPubBlackboard().Get<float>("冰") == 3)
                {
                    Game.Event.Fire(this, KaerQihuanEventArgs.Create(this, 10009));
                }
                else if (GetPubBlackboard().Get<float>("冰") == 2 && GetPubBlackboard().Get<float>("火") == 1)
                {
                    Game.Event.Fire(this, KaerQihuanEventArgs.Create(this, 10017));
                }
                else if (GetPubBlackboard().Get<float>("冰") == 2 && GetPubBlackboard().Get<float>("雷") == 1)
                {
                    Game.Event.Fire(this, KaerQihuanEventArgs.Create(this, 10010));
                }
                else if (GetPubBlackboard().Get<float>("火") == 3)
                {
                    Game.Event.Fire(this, KaerQihuanEventArgs.Create(this, 10015));
                }
                else if (GetPubBlackboard().Get<float>("雷") == 3)
                {
                    Game.Event.Fire(this, KaerQihuanEventArgs.Create(this, 10012));
                }
                else if (GetPubBlackboard().Get<float>("火") == 2 && GetPubBlackboard().Get<float>("雷") == 1)
                {
                    Game.Event.Fire(this, KaerQihuanEventArgs.Create(this, 10014));
                }
                else if (GetPubBlackboard().Get<float>("火") == 1 && GetPubBlackboard().Get<float>("雷") == 2)
                {
                    Game.Event.Fire(this, KaerQihuanEventArgs.Create(this, 10013));
                }
                else if (GetPubBlackboard().Get<float>("火") == 1 && GetPubBlackboard().Get<float>("雷") == 1
                    && GetPubBlackboard().Get<float>("冰") == 1)
                {
                    Game.Event.Fire(this, KaerQihuanEventArgs.Create(this, 10018));
                }
            }
        }
    }
}
