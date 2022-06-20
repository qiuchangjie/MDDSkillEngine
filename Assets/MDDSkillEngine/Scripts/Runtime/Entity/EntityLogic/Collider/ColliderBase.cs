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

            if (data != null)
            {
                if (data.targetType == TargetType.NONE)
                {
                    if (data.Owner != null)
                    {
                        Game.Entity.AttachEntity(Id, data.Owner.Id);
                        CachedTransform.localRotation = data.localRotation;
                        CachedTransform.localPosition = data.localeftPostion;
                        CachedTransform.localScale = data.localScale;

                        if (!data.IsFollowParent)
                        {
                            Game.Entity.DetachEntity(Id);
                        }
                    }
                    
                }
                else if (data.targetType == TargetType.POINT)
                {
                    CachedTransform.localRotation = Game.Select.mouseTarget.transform.rotation;
                    CachedTransform.localPosition = Game.Select.mouseTarget.transform.position;
                    CachedTransform.localScale = data.localScale;
                }
               

                if (data.useSpeed && data.hasPath)
                {
                    bezierPath.Clear();

                    if (data.targetType == TargetType.POINT)//如果是点目标则按照鼠标摆放位置转换坐标
                    {
                        //坐标转换 将曲线本地坐标转换为世界坐标
                        for (int i = 0; i < data.bezierPath.Length; i++)
                        {
                            Vector3 vec3;
                            vec3 = Game.Select.mouseTarget.transform.TransformPoint(data.bezierPath[i]);
                            bezierPath.Add(vec3);
                        }
                    }
                    else if (data.targetType == TargetType.NONE)
                    {
                        //坐标转换 将曲线本地坐标转换为世界坐标
                        for (int i = 0; i < data.bezierPath.Length; i++)
                        {
                            Vector3 vec3;
                            vec3 = data.Owner.CachedTransform.TransformPoint(data.bezierPath[i]);
                            bezierPath.Add(vec3);
                        }
                    }

                    
                }
            }        
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);

            if (data != null)
            {
                if (data.useSpeed && data.hasPath)
                {
                    CachedTransform.position = AIUtility.GetPoint(wasDuration / data.Duration, data.bezierPathLength, bezierPath);
                }

                if (data.Duration <= wasDuration)
                {
                    HideSelf();
                }
            }          
        }
    }
}
