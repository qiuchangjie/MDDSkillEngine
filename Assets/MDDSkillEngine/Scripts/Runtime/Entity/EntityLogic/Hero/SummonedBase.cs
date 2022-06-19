using UnityEngine;
using MDDGameFramework.Runtime;
using MDDGameFramework;

namespace MDDSkillEngine
{
    /// <summary>
    /// 召唤物实体类。
    /// </summary>
    public abstract class SummonedBase : Entity
    {
        [SerializeField]
        public SummonedBaseData m_SummonedBaseData = null;

        public bool IsDead
        {
            get
            {
                return m_SummonedBaseData.HP <= 0;
            }
        }

        public abstract ImpactData GetImpactData();

        public void ApplyDamage(Entity attacker, int damageHP)
        {
            if (damageHP > 0)
                Game.TextBar.ShowTextBar(this, damageHP);


            float fromHPRatio = m_SummonedBaseData.HPRatio;
            m_SummonedBaseData.HP -= damageHP;
            float toHPRatio = m_SummonedBaseData.HPRatio;
            if (fromHPRatio > toHPRatio)
            {
                Game.HpBar.ShowHPBar(this, fromHPRatio, toHPRatio);
            }

            if (m_SummonedBaseData.HP <= 0)
            {
                OnDead(attacker);
            }
        }


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_SummonedBaseData = userData as SummonedBaseData;
            if (m_SummonedBaseData == null)
            {
                Log.Error("Targetable object data is invalid.");
                return;
            }

            m_SummonedBaseData.HP = 666;
        }

        protected virtual void OnDead(Entity attacker)
        {
            Log.Error("ai死亡！！！！！！！！");
        }
    }
}
