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
