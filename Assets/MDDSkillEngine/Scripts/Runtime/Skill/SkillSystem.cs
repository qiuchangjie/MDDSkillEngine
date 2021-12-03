using MDDGameFramework;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    public class SkillSystem : ISkillSystem, IReference
    {
        private Dictionary<NameNamePair, Skill> skillDic;

        private Entity m_Owner;

        public Entity Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public SkillSystem()
        {
            skillDic = new Dictionary<NameNamePair, Skill>();
        }


        public static SkillSystem Create(Entity Owner)
        {
            SkillSystem sys = ReferencePool.Acquire<SkillSystem>();
            sys.m_Owner = Owner;

            return sys;
        }

        public void AddSkill(string name)
        {
            //SkillFactory.AcquireSkill();
        }

        public void AddSkill(int skillId)
        {
            Skill skill = SkillFactory.AcquireSkill(skillId, m_Owner);

            if (skill == null)
            {
                Log.Error("技能创建失败id：{0}", skillId);
            }
            else
            {
                Log.Info("添加技能成功：{0}", skillId);
            }

            skill.Start();
            skillDic.Add(new NameNamePair(skillId.ToString(), m_Owner.Id.ToString()), skill);
        }

        public Skill GetSkill(int id)
        {
            Skill skill;
            if (!skillDic.TryGetValue(new NameNamePair(id.ToString(),m_Owner.Id.ToString()), out skill))
            {
                Log.Error("尝试获取没有装配的技能id：{0}",id);
                return null;
            }

            return skill;
        }
       
        public void RemoveSkill(string name)
        {
            
        }

        public void UpgradeSkill(string name)
        {
            
        }

        public void Clear()
        {

        }
    }
}
