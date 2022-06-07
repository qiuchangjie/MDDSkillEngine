using UnityEngine;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    /// <summary>
    /// 特效类。
    /// </summary>
    public class Effect : EffectBase
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
            {
                if (m_EffectData.IsFllow)
                {
                    Game.Entity.AttachEntity(Id, m_EffectData.Owner.Id);
                    CachedTransform.localRotation = m_EffectData.localRotation;
                    CachedTransform.localPosition = m_EffectData.localeftPostion;
                    CachedTransform.localScale = m_EffectData.localScale;
                }
                else
                {
                    Game.Entity.AttachEntity(Id, m_EffectData.Owner.Id);
                    CachedTransform.localRotation = m_EffectData.localRotation;
                    CachedTransform.localPosition = m_EffectData.localeftPostion;
                    CachedTransform.localScale = m_EffectData.localScale;
                    Game.Entity.DetachEntity(Id);
                }              
            }

        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            m_ElapseSeconds += elapseSeconds;

            if (m_EffectData.IsFllow)
            {
                CachedTransform.position = m_EffectData.Owner.CachedTransform.position;
            }

            //if (m_EffectData.hasPath)
            //{
            //    CachedTransform.position = AIUtility.GetPoint(m_ElapseSeconds,m_EffectData.bezierPathLength,m_EffectData.bezierPath);
            //}

            if (m_ElapseSeconds >= m_EffectData.KeepTime)
            {
                Game.Entity.HideEntity(this);
            }



            
        }
    }
}
