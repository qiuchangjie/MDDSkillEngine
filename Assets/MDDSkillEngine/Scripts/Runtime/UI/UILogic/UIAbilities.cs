using MDDGameFramework;
using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace MDDSkillEngine
{
    public class UIAbilities : UGuiForm
    {
        /// <summary>
        /// 显示的实体
        /// </summary>
        Entity m_Entity;

        public List<AblitiesSlot> ablitiesSlots = new List<AblitiesSlot>();

        public List<AblitiesSlot> kaerablitiesSlots = new List<AblitiesSlot>();

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);

            for (int i = 0; i < ablitiesSlots.Count; i++)
            {
                ablitiesSlots[i].Init(null);
                ablitiesSlots[i].InitIndex(i);            
            }

            for (int i = 0; i < kaerablitiesSlots.Count; i++)
            {
                kaerablitiesSlots[i].Init(null);
            }

            Game.Input.Control.Heros_Normal.Skill_1.performed += UseSkill_1;
            Game.Input.Control.Heros_Normal.Skill_2.performed += UseSkill_2;
            Game.Input.Control.Heros_Normal.Skill_3.performed += UseSkill_3;
            Game.Input.Control.Heros_Normal.Skill_4.performed += UseSkill_4;
            Game.Input.Control.Heros_Normal.Skill_5.performed += UseSkill_5;
            Game.Input.Control.Heros_Normal.Skill_6.performed += UseSkill_6;

            Game.Event.Subscribe(AddSkillEventArgs.EventId, LearnedSkill);
            Game.Event.Subscribe(SelectEntityEventArgs.EventId, SwitchEntity);
            Game.Event.Subscribe(KaerQihuanEventArgs.EventId, KaerQihuan);
        }


        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            Log.Info("{0}ui关闭name：{1}", LogConst.UI, Name);

            Game.Event.Unsubscribe(AddSkillEventArgs.EventId, LearnedSkill);
            Game.Event.Unsubscribe(SelectEntityEventArgs.EventId, SwitchEntity);
            Game.Event.Unsubscribe(KaerQihuanEventArgs.EventId, KaerQihuan);
        }

        public void SetEntity(Entity entity)
        {
            if (m_Entity != entity)
            {
                m_Entity = entity;
                ISkillSystem skillSystem = Game.Skill.GetSkillSystem(entity.Id);
                if (skillSystem != null)
                {
                    if (skillSystem is KealSkillSystem)
                    {
                        for (int i = 0; i < kaerablitiesSlots.Count; i++)
                        {
                            kaerablitiesSlots[i].gameObject.SetActive(true);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < kaerablitiesSlots.Count; i++)
                        {
                            kaerablitiesSlots[i].gameObject.SetActive(false);
                        }
                    }

                    Log.Info("{0}切换：{1}", LogConst.UI, skillSystem.GetType());

                    for (int i = 0; i < ablitiesSlots.Count; i++)
                    {
                        ablitiesSlots[i].SwitchSkillSystem(skillSystem);
                    }
                }
            }
        }

        public void LearnSkill(int index, int Skillid)
        {
            ISkillSystem s = Game.Skill.GetSkillSystem(m_Entity.Id);
            s.AddSkill(Skillid, index);
        }

        private void LearnedSkill(object sender, GameEventArgs e)
        {
            AddSkillEventArgs a = e as AddSkillEventArgs;

            if (a.Index == -1)
            {
                return;
            }

            if (a != null && a.SkillSystem == Game.Skill.GetSkillSystem(m_Entity.Id))
            {
                ablitiesSlots[a.Index].Init(a.SkillSystem, a.SkillID);
            }
        }

        private void KaerQihuan(object sender, GameEventArgs e)
        {
            KaerQihuanEventArgs a = e as KaerQihuanEventArgs;
            if (a != null && a.SkillSystem == Game.Skill.GetSkillSystem(m_Entity.Id))
            {
                if (kaerablitiesSlots[0].SkillID == a.SkillID)
                    return;

                if (kaerablitiesSlots[0].SkillID != 0)
                    kaerablitiesSlots[1].Init(kaerablitiesSlots[0].m_SkillSystem, kaerablitiesSlots[0].SkillID);

                kaerablitiesSlots[0].Init(a.SkillSystem, a.SkillID);
            }
        }

        private void SwitchEntity(object sender, GameEventArgs n)
        {
            SelectEntityEventArgs e = n as SelectEntityEventArgs;

            SetEntity(e.entity);
        }

        public void UseSkill_1(CallbackContext ctx)
        {
            ablitiesSlots[0].UseSkill();
        }

        public void UseSkill_2(CallbackContext ctx)
        {
            ablitiesSlots[1].UseSkill();
        }

        public void UseSkill_3(CallbackContext ctx)
        {
            ablitiesSlots[2].UseSkill();
        }

        public void UseSkill_4(CallbackContext ctx)
        {
            ablitiesSlots[3].UseSkill();
        }

        public void UseSkill_5(CallbackContext ctx)
        {
            kaerablitiesSlots[0].UseSkill();
        }

        public void UseSkill_6(CallbackContext ctx)
        {
            kaerablitiesSlots[1].UseSkill();
        }
    }

}

