using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace MDDSkillEngine
{
    public class InputSystemComponent : MDDGameFrameworkComponent
    {

        private MDDInputControls m_Control;

        public MDDInputControls Control
        {
            get
            {
                return m_Control;
            }
        }


        void Start()
        {
            m_Control = new MDDInputControls();

            m_Control.Enable();
        }


    }
}


