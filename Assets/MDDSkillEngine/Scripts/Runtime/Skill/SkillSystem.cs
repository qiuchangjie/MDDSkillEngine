using MDDGameFramework;
using System.Collections.Generic;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class SkillSystem<T> : ISkillSystem, IReference where T : Entity
    {
        private Dictionary<NameNamePair, Skill> skillDic;

        /// <summary>
        /// 技能系统关联的公共黑板
        /// </summary>
        private Blackboard m_PublicBlackboard;

        /// <summary>
        /// 技能系统所有者
        /// </summary>
        protected T m_Owner;

        public T Owner
        {
            get
            {
                return m_Owner;
            }
        }
        

        /// <summary>
        /// 用来记录当前技能释放的结果
        /// </summary>
        public SkillReleaseResultType CurrentSkillState = SkillReleaseResultType.NONE;



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
                Log.Info("{1}添加技能成功：{0}", skillId, LogConst.Skill);
            }

            skill.Start();
            skillDic.Add(new NameNamePair(skillId.ToString(), m_Owner.Id.ToString()), skill);
        }

     
        public Skill GetSkill(int id)
        {
            Skill skill;
            if (!skillDic.TryGetValue(new NameNamePair(id.ToString(), m_Owner.Id.ToString()), out skill))
            {
                Log.Error("{1}尝试获取没有装配的技能id：{0}", id, LogConst.Skill);
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
            IFsm<Entity> fsm = Game.Fsm.GetFsm<Entity>(m_Owner.Id.ToString());
            IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();
            DRSkill drSkill = dtSkill.GetDataRow(id);

            //通过技能id对应到关联的state实例
            if (drSkill.IsState)
            {
                fsm.SetData<VarBoolean>(drSkill.StateName, true);
            }
            else
            {
                SetSkillReleaseResultType(SkillReleaseResultType.FAIL);
                Log.Error("{0}Skill{1}未关联state", LogConst.Skill, id);
            }
        }

        /// <summary>
        /// 请求当前技能的释放结果
        /// </summary>
        /// <param name="skillID"></param>
        /// <returns></returns>
        public SkillReleaseResultType GetSkillReleaseResultType()
        {
            return CurrentSkillState;         
        }

        /// <summary>
        /// 设置当前技能释放结果
        /// </summary>
        /// <param name="skillReleaseResultType"></param>
        public void SetSkillReleaseResultType(SkillReleaseResultType skillReleaseResultType)
        {
            Log.Info("{0}设置技能释放状态，{1}", LogConst.Skill, skillReleaseResultType);
            CurrentSkillState = skillReleaseResultType;
        }

      
        public virtual void RemoveSkill(string name)
        {

        }

        /// <summary>
        /// 升级技能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool UpgradeSkill(int id)
        {
            return false;
        }

        /// <summary>
        /// 获取技能级黑板
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Blackboard GetSkillBlackboard(int id)
        {
            Skill skill;
            if (!skillDic.TryGetValue(new NameNamePair(id.ToString(), m_Owner.Id.ToString()), out skill))
            {
                Log.Error("尝试获取没有装配的技能id：{0}", id);
            }

            return skill.Blackboard;
        }
        
        /// <summary>
        /// 获取公共黑板
        /// </summary>
        /// <returns></returns>
        public Blackboard GetPubBlackboard()
        {
            return m_PublicBlackboard;
        }

        /// <summary>
        /// 设置公共黑板
        /// </summary>
        /// <param name="blackboard"></param>
        public void SetBlackboard(Blackboard blackboard)
        {
            m_PublicBlackboard = blackboard;
        }

        public void Clear()
        {

        }


    }

    public enum SkillReleaseResultType
    {
        /// <summary>
        /// 无技能被释放
        /// </summary>
        NONE,
        /// <summary>
        /// 技能释放成功
        /// </summary>
        SUCCSE,
        /// <summary>
        /// 技能释放了 但是在进入cd前被中止
        /// </summary>
        STOP,
        /// <summary>
        /// 技能释放失败
        /// </summary>
        FAIL,
        /// <summary>
        /// 技能在释放过程中
        /// </summary>
        PROGRESS,
    }
}
