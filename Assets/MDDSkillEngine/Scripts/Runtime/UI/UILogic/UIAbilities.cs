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
            }

            Game.Input.Control.Heros_Normal.Skill_1.performed += UseSkill_1;
            Game.Input.Control.Heros_Normal.Skill_1.performed += UseSkill_2;
            Game.Input.Control.Heros_Normal.Skill_1.performed += UseSkill_3;
            Game.Input.Control.Heros_Normal.Skill_1.performed += UseSkill_4;

            Game.Event.Subscribe(AddSkillEventArgs.EventId, LearnedSkill);
        }


        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            Log.Info("{0}ui关闭name：{1}", LogConst.UI, Name);

            Game.Event.Unsubscribe(AddSkillEventArgs.EventId, LearnedSkill);
        }

        public void SetEntity(Entity entity)
        {
            m_Entity = entity;
        }

        public void LearnSkill(int index, int Skillid)
        {
            ISkillSystem s = Game.Skill.GetSkillSystem(m_Entity.Id);
            s.AddSkill(Skillid, index);
        }

        private void LearnedSkill(object sender, GameEventArgs e)
        {
            AddSkillEventArgs a=e as AddSkillEventArgs;
            if (a != null && a.SkillSystem == Game.Skill.GetSkillSystem(m_Entity.Id))
            {
                ablitiesSlots[a.Index].Init(a.SkillSystem,a.SkillID);
            }          
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
    }

}

