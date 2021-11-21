using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;

namespace MDDSkillEngine
{
    public class DubaoData : BuffDatabase
    {
        private int m_damage;

        public int Damage
        {
            get { return m_damage; }
            set { m_damage = value; }
        }

        public void Init(DRBuff dRBuff)
        {
            Init(dRBuff.Id, dRBuff.Level, dRBuff.Duration);

            m_damage = dRBuff.OverDamage;
        }

    }

}

