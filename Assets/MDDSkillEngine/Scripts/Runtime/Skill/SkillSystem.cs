using MDDGameFramework;
using System.Collections.Generic;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class SkillSystem<T> : ISkillSystem, IReference where T : Entity
    {
        private Dictionary<int, Skill> skillDic;

        /// <summary>
        /// 位置映射表
        /// 因为没做存档机制 
        /// 用于临时存储技能在技能ui上的位置信息
        /// </summary>
        private Dictionary<int, int> skillIndex;

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
            skillDic = new Dictionary<int, Skill>();
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
            skillDic.Add(skillId, skill);

            Game.Event.Fire(this, AddSkillEventArgs.Create(this, skillId, -1));
        }

        public void AddSkill(int skillId, int index)
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
            skillDic.Add(skillId, skill);

            Game.Event.Fire(this, AddSkillEventArgs.Create(this, skillId, index));
        }




        public Skill GetSkill(int id)
        {
            Skill skill;
            if (!skillDic.TryGetValue(id, out skill))
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
            if (!skillDic.TryGetValue(id, out skill))
            {
                Log.Error("尝试获取没有装配的技能id：{0}", id);
            }
            skill.Blackboard.Set<VarBoolean>("input", true);

            Game.Event.Fire(this, UseSkillEventArgs.Create(this, id));
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

            Game.Event.Fire(this, ReleaseSkillEventArgs.Create(this, id));
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


        public void RemoveSkill(int id)
        {
            if (skillDic.TryGetValue(id, out Skill skill))
            {
                skillDic.Remove(id);
                IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();
                DRSkill drSkill = dtSkill.GetDataRow(id);
                Game.NPBehave.RemoveBehaviourTree(new NameNamePair(drSkill.AssetName,m_Owner.Id.ToString()));
                Game.Event.Fire(this, RemoveSkillEventArgs.Create(this, id));
            }
            else
            {
                Log.Error("{0}尝试移除未装配技能，id：{1}", LogConst.Skill, id);
            }
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
            if (!skillDic.TryGetValue(id, out skill))
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

    /// <summary>
    /// 技能释放结果类型
    /// </summary>
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

    public enum TargetType
    {
        NONE,
        SELF,
        POINT,
        ENTITY,
    }
}
