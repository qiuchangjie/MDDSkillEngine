

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using MDDGameFramework;

namespace MDDSkillEngine
{
    /// <summary>
    /// AI 工具类。
    /// </summary>
    public static class AIUtility
    {

        /// <summary>
        /// 按照贝塞尔曲线编辑路径运行 runtime函数
        /// </summary>
        /// <param name="t"></param>
        /// <param name="length"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Vector3 GetPoint(float t, float length,List<Vector3> path)
        {
            if (t <= 0) return path[0];
            if (t >= 1) return path[path.Count - 1];
            var a = Vector3.zero;
            var b = Vector3.zero;
            var total = 0f;
            var segmentDistance = 0f;
            var pathLength = length;
            for (var i = 0; i < path.Count - 1; i++)
            {
                segmentDistance = Vector3.Distance(path[i], path[i + 1]) / pathLength;
                if (total + segmentDistance > t)
                {
                    a = path[i];
                    b = path[i + 1];
                    break;
                }
                else
                {
                    total += segmentDistance;
                }
            }
            t -= total;
            return Vector3.Lerp(a, b, t / segmentDistance);
        }

        /// <summary>
        /// 获取实体间的距离。
        /// </summary>
        /// <returns>实体间的距离。</returns>
        public static float GetDistance(Entity fromEntity, Entity toEntity)
        {
            Transform fromTransform = fromEntity.CachedTransform;
            Transform toTransform = toEntity.CachedTransform;
            return (toTransform.position - fromTransform.position).magnitude;
        }

        public static void TakeDamage(Entity other)
        {
            
        }


        public static void PerformCollision(TargetableObject entity, Entity other)
        {
            if (entity == null || other == null)
            {
                return;
            }

            TargetableObject target = other as TargetableObject;
            if (target != null)
            {
                ImpactData entityImpactData = entity.GetImpactData();
                ImpactData targetImpactData = target.GetImpactData();
               

                int entityDamageHP = CalcDamageHP(targetImpactData.Attack, 0);
                int targetDamageHP = CalcDamageHP(entityImpactData.Attack, 0);

                int delta = entityDamageHP;

                Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70003)
                {
                    Position = entity.CachedTransform.position,
                    Rotation = entity.CachedTransform.rotation
                });



                entity.ApplyDamage(target, entityDamageHP);
                //target.ApplyDamage(entity, targetDamageHP);
                return;
            }

            //Bullet bullet = other as Bullet;
            //if (bullet != null)
            //{
            //    ImpactData entityImpactData = entity.GetImpactData();
            //    ImpactData bulletImpactData = bullet.GetImpactData();
              

            //    int entityDamageHP = CalcDamageHP(bulletImpactData.Attack, entityImpactData.Defense);

            //    entity.ApplyDamage(bullet, entityDamageHP);
            //    GameEntry.Entity.HideEntity(bullet);
            //    return;
            //}
        }

        public static int CalcDamageHP(int attack, int defense)
        {
            if (attack <= 0)
            {
                return 0;
            }

            if (defense < 0)
            {
                defense = 0;
            }

            return attack ;
        }

        [StructLayout(LayoutKind.Auto)]
        private struct CampPair
        {
            private readonly CampType m_First;
            private readonly CampType m_Second;

            public CampPair(CampType first, CampType second)
            {
                m_First = first;
                m_Second = second;
            }

            public CampType First
            {
                get
                {
                    return m_First;
                }
            }

            public CampType Second
            {
                get
                {
                    return m_Second;
                }
            }
        }
    }
}
