using UnityEngine;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    /// <summary>
    /// 特效类。
    /// </summary>
    public class Effect : Entity
    {
        [SerializeField]
        private EffectData m_EffectData = null;

        private float m_ElapseSeconds = 0f;


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_EffectData = userData as EffectData;
            if (m_EffectData == null)
            {
                Log.Error("Effect data is invalid.");
                return;
            }

            m_ElapseSeconds = 0f;

            if (m_EffectData.Owner != null)
                Game.Entity.AttachEntity(Id, m_EffectData.Owner.Id);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            m_ElapseSeconds += elapseSeconds;

            if (m_EffectData.IsFllow)
            {
                CachedTransform.position = m_EffectData.Owner.CachedTransform.position;
            }

            if (m_ElapseSeconds >= m_EffectData.KeepTime)
            {
                Game.Entity.HideEntity(this);
            }

            
        }
    }
}
