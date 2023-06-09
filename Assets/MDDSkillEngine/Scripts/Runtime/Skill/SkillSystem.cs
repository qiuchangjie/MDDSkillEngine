﻿using MDDGameFramework;
using System.Collections.Generic;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class SkillSystem<T> : ISkillSystem, IReference where T : Entity
    {
        /// <summary>
        /// 角色已经添加的技能库
        /// </summary>
        protected Dictionary<int, Skill> skillDic;

        /// <summary>
        /// 位置映射表
        /// 因为没做存档机制 
        /// 用于临时存储技能在技能ui上的位置信息
        /// <skillid, index>
        /// </summary>
        protected Dictionary<int, int> skillIndex;

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


        public virtual Dictionary<int, int> SkillIndex
        {
            get { return skillIndex; }
        }


        /// <summary>
        /// 用来记录当前技能释放的结果
        /// </summary>
        public SkillReleaseResultType CurrentSkillState = SkillReleaseResultType.NONE;

        public SkillSystem()
        {
            
        }


        public static SkillSystem<T> Create(Entity Owner)
        {
            SkillSystem<T> sys = ReferencePool.Acquire<SkillSystem<T>>();
            sys.m_Owner = Owner as T;
            sys.skillDic = new Dictionary<int, Skill>();
            sys.skillIndex = new Dictionary<int, int>()
            {
                {0, 0},
                {1, 0},
                {2, 0},
                {3, 0},
            };
            return sys;
        }

        /// <summary>
        /// 向技能系统中添加技能
        /// </summary>
        /// <param name="skillId"></param>
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

        /// <summary>
        /// 向技能系统中添加技能
        /// </summary>
        /// <param name="skillId"></param>
        /// <param name="index"></param>
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
            SkillIndex[index] = skillId;
            Game.Event.Fire(this, AddSkillEventArgs.Create(this, skillId, index));
        }



        /// <summary>
        /// 获取一个技能
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        public virtual void ReleaseSkill(int id)
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
                SetSkillReleaseResultType(SkillReleaseResultType.PROGRESS);
                Log.Info("{0}Skill{1}未关联state", LogConst.Skill, id);
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

        public void RemoveSkill(int id,int index)
        {
            if (skillDic.TryGetValue(id, out Skill skill))
            {
                skillDic.Remove(id);
                skillIndex[index] = 0;
                IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();
                DRSkill drSkill = dtSkill.GetDataRow(id);
                Game.NPBehave.RemoveBehaviourTree(new NameNamePair(drSkill.AssetName, m_Owner.Id.ToString()));
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

        public void Shutdown()
        {
            ReferencePool.Release(this);
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

        /// <summary>
        /// 技能系统回收
        /// 回收的同时也要连带技能行为树的回收
        /// </summary>
        public void Clear()
        {        
            if (m_PublicBlackboard != null)
            {
                ReferencePool.Release(m_PublicBlackboard);
                m_PublicBlackboard = null;
            }
          
            foreach (var v in skillDic)
            {
                IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();
                DRSkill drSkill = dtSkill.GetDataRow(v.Key);
                Game.NPBehave.RemoveBehaviourTree(new NameNamePair(drSkill.AssetName, m_Owner.Id.ToString()));
                ReferencePool.Release(v.Value);
            }

            m_Owner = null;
            skillDic = null;
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
        /// <summary>
        /// 无
        /// </summary>
        NONE,
        /// <summary>
        /// 自身
        /// </summary>
        SELF,
        /// <summary>
        /// 点目标
        /// </summary>
        POINT,
        /// <summary>
        /// 具体实体目标
        /// </summary>
        ENTITY,
    }
}
