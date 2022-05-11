using MDDGameFramework;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    public class SkillSystem<T> : ISkillSystem, IReference where T : Entity
    {
        private Dictionary<NameNamePair, Skill> skillDic;

        private Blackboard PublicBlackboard;

        private T m_Owner;

        public T Owner
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


        public static SkillSystem<T> Create(Entity Owner)
        {
            SkillSystem<T> sys = ReferencePool.Acquire<SkillSystem<T>>();
            sys.m_Owner = Owner as T;

            return sys;
        }


        public void AddSkill(int skillId)
        {
            Skill skill = SkillFactory.AcquireSkill(skillId, m_Owner as Entity);

            if (skill == null)
            {
                Log.Error("技能创建失败id：{0}", skillId);
            }
            else
            {
                Log.Info("{1}添加技能成功：{0}", skillId,LogConst.Skill);
            }

            skill.Start();
            skillDic.Add(new NameNamePair(skillId.ToString(), m_Owner.Id.ToString()), skill);
        }

        public Skill GetSkill(int id)
        {
            Skill skill;
            if (!skillDic.TryGetValue(new NameNamePair(id.ToString(), m_Owner.Id.ToString()), out skill))
            {
                Log.Error("{1}尝试获取没有装配的技能id：{0}", id,LogConst.Skill);
                return null;
            }

            return skill;
        }

        /// <summary>
        /// 开始使用技能
        /// 不代表技能的主要逻辑被释放
        /// </summary>
        /// <param name="id"></param>
        public void UseSkill(int id)
        {
            Skill skill;
            if (!skillDic.TryGetValue(new NameNamePair(id.ToString(), m_Owner.Id.ToString()), out skill))
            {
                Log.Error("尝试获取没有装配的技能id：{0}", id);
            }
            skill.Blackboard.Set<VarBoolean>("input", true);
        }


        /// <summary>
        /// 释放技能
        /// </summary>
        /// <param name="id"></param>
        public void ReleaseSkill(int id)
        {
            IFsm<T> fsm = Game.Fsm.GetFsm<T>(m_Owner.Id.ToString());
            //这里需要做成通通过id索引到对应的技能关联状态名称 
            fsm.SetData<VarBoolean>("shunxi", true);
        }

        public virtual void RemoveSkill(string name)
        {

        }

        public virtual bool UpgradeSkill(int id)
        {
            return false;
        }

        public void Clear()
        {

        }


    }
}
