using UnityEngine;
using MDDGameFramework.Runtime;
using MDDGameFramework;

namespace MDDSkillEngine
{
    /// <summary>
    /// 特效类。
    /// </summary>
    public class Effect : EffectBase
    {
        ParticleSystem pat;
      
        protected override void OnShow(object userData)
        {
            m_EffectData = userData as EffectData;

            base.OnShow(userData);
       
            if (m_EffectData == null)
            {
                Log.Error("Effect data is invalid.");
                return;
            }

            pat = GetComponent<ParticleSystem>();
      
            if (m_EffectData.Owner != null)
            {
                var main = pat.main;
                main.simulationSpeed = m_EffectData.Owner.blackboard.Get<float>("PlaybleSpeed");
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
        }

        public override void ObservingPlayableSpeed(Blackboard.Type type, Variable newValue)
        {
            base.ObservingPlayableSpeed(type, newValue);
            if (type == Blackboard.Type.CHANGE)
            {
                VarFloat varFloat = (VarFloat)newValue;
                if (pat != null)
                {
                    var main = pat.main;
                    main.simulationSpeed = varFloat;
                }
            }
        }
    }
}
