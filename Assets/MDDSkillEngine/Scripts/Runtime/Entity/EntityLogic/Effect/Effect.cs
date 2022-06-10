using UnityEngine;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    /// <summary>
    /// 特效类。
    /// </summary>
    public class Effect : EffectBase
    {
      
        protected override void OnShow(object userData)
        {

            m_EffectData = userData as EffectData;

            base.OnShow(userData);
       
            if (m_EffectData == null)
            {
                Log.Error("Effect data is invalid.");
                return;
            }

       
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }
    }
}
