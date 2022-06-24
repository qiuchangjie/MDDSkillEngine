﻿using MDDGameFramework;
using UnityEngine;
using UnityEngine.UI;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    public class AblitiesSlot : MonoBehaviour
    {
        public ISkillSystem m_SkillSystem;
        public Image CDImage;
        public Text Key;
        public Text SkillName;
        public int SkillID = 0;
        public int index = 0;

        public void Start()
        {

        }

        public void InitIndex(int index)
        {
            this.index = index;
        }

        public void Init(ISkillSystem skillSystem,int skillid = 0)
        {
            if (skillid == 0)
            {
                m_SkillSystem = null;
                SkillName.text = "";
                CDImage.fillAmount = 0;
                SkillID = 0;
            }
            else
            {
                if (SkillID != 0)
                    m_SkillSystem.GetSkillBlackboard(SkillID).RemoveObserver("cd", Observing);


                IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();
                DRSkill drSkill = dtSkill.GetDataRow(skillid);               
                m_SkillSystem = skillSystem;
                SkillName.text=drSkill.Name;
                this.SkillID = skillid;

                float cdtime = m_SkillSystem.GetSkillBlackboard(SkillID).Get<float>("cdtime");
                float cd = m_SkillSystem.GetSkillBlackboard(SkillID).Get<float>("cd");

                if (cdtime == 0)
                {
                    CDImage.fillAmount = 0;
                }
                else
                {
                    float amount = cd / cdtime;
                    CDImage.fillAmount = amount;
                }
               

                m_SkillSystem.GetSkillBlackboard(SkillID).AddObserver("cd", Observing);
            }
        }

        public void UseSkill()
        {
            if (this.SkillID != 0)
            {
                m_SkillSystem.UseSkill(SkillID);
            }
        }

        public void SwitchSkillSystem(ISkillSystem skillSystem)
        {
            if (skillSystem == m_SkillSystem)
                return;

            //if (SkillID != 0)
            //    m_SkillSystem.GetSkillBlackboard(SkillID).RemoveObserver("cd", Observing);


            Init(skillSystem, skillSystem.SkillIndex[index]);                    
        }

        private void Observing(Blackboard.Type type, Variable newValue)
        {
            //Log.Info("chang{0}", type);
            if (type == Blackboard.Type.CHANGE)
            {
                float cdtime = m_SkillSystem.GetSkillBlackboard(SkillID).Get<float>("cdtime");
                float cd = (VarFloat)newValue;

                float amount = cd / cdtime;
                CDImage.fillAmount = amount;
            }
        }
    }
}


