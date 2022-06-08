using UnityEngine;
using MDDGameFramework.Runtime;
using MDDGameFramework;
using System.Collections.Generic;

namespace MDDSkillEngine
{
    /// <summary>
    /// 碰撞体实体基类。
    /// </summary>
    public abstract class ColliderBase : Entity
    {
        public ColliderData data;

        List<Vector3> bezierPath = new List<Vector3>();

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            if (data.useSpeed&&data.hasPath)
            {
                bezierPath.Clear();

                //坐标转换 将曲线本地坐标转换为世界坐标
                for (int i = 0; i < data.bezierPath.Length; i++)
                {
                    Vector3 vec3;
                    vec3 = data.Owner.CachedTransform.TransformPoint(data.bezierPath[i]);
                    bezierPath.Add(vec3);
                }
            }
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (data.useSpeed && data.hasPath)
            {
                CachedTransform.position = AIUtility.GetPoint(1 / data.Duration, data.bezierPathLength, bezierPath);
            }
        }
    }
}
