using UnityEngine;
using MDDGameFramework.Runtime;
using MDDGameFramework;

namespace MDDSkillEngine
{
    /// <summary>
    /// 可作为目标的实体类。
    /// </summary>
    public abstract class TargetableObject : Entity
    {
        [SerializeField]
        public TargetableObjectData m_TargetableObjectData = null;

        public bool IsDead
        {
            get
            {
                return m_TargetableObjectData.HP <= 0;
            }
        }

        public abstract ImpactData GetImpactData();

        public void ApplyDamage(Entity attacker, int damageHP)
        {
            if (damageHP > 0)
                Game.TextBar.ShowTextBar(this, damageHP);

                   
            float fromHPRatio = m_TargetableObjectData.HPRatio;
            m_TargetableObjectData.HP -= damageHP;
            float toHPRatio = m_TargetableObjectData.HPRatio;
            if (fromHPRatio > toHPRatio)
            {
                Game.HpBar.ShowHPBar(this, fromHPRatio, toHPRatio);
            }

            if (m_TargetableObjectData.HP <= 0)
            {
                OnDead(attacker);
            }
        }


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            //gameObject.SetLayerRecursively(Constant.Layer.TargetableObjectLayerId);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            
            m_TargetableObjectData = userData as TargetableObjectData;
            if (m_TargetableObjectData == null)
            {
                //Log.Error("Targetable object data is invalid.");
                return;
            }

            m_TargetableObjectData.HP = 666;
        }

        protected virtual void OnDead(Entity attacker)
        {
            Log.Error("ai死亡！！！！！！！！");

            //Game.Fsm.GetFsm<Enemy>(Id.ToString()).SetData<VarBoolean>("died", true);
        }    
    }
}
