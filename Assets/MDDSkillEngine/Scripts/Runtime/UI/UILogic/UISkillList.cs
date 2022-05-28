using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MDDGameFramework.Runtime;
using MDDGameFramework;

namespace MDDSkillEngine
{
    public class UISkillList : UGuiForm
    {

        public SkillSlot SlotInstance;

        [SerializeField]
        private Transform insTransform;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();

            foreach (var item in dtSkill)
            {
                SkillSlot s = Instantiate(SlotInstance, insTransform);
                s.Init(item);
            }         
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
        }


    }

}

