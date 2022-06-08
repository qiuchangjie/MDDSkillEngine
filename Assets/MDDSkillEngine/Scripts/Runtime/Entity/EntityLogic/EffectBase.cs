using UnityEngine;
using MDDGameFramework.Runtime;
using MDDGameFramework;
using System.Collections.Generic;

namespace MDDSkillEngine
{
    /// <summary>
    /// 技能实体基类。
    /// </summary>
    public abstract class EffectBase : Entity
    {
        protected EffectData m_EffectData = null;

        List<Vector3> bezierPath = new List<Vector3>();

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            if (m_EffectData.useSpeed && m_EffectData.hasPath)
            {
                bezierPath.Clear();

                //坐标转换 将曲线本地坐标转换为世界坐标
                for (int i = 0; i < m_EffectData.bezierPath.Length; i++)
                {
                    Vector3 vec3;
                    vec3 = m_EffectData.Owner.CachedTransform.TransformPoint(m_EffectData.bezierPath[i]);
                    bezierPath.Add(vec3);
                }
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (m_EffectData.useSpeed && m_EffectData.hasPath)
            {
                CachedTransform.position = AIUtility.GetPoint(1 / m_EffectData.KeepTime, m_EffectData.bezierPathLength, bezierPath);
            }
        }
    }
}
