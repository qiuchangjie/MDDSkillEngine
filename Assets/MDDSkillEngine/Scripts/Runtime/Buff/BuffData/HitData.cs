using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MDDGameFramework;

namespace MDDSkillEngine
{
    public class HitData:IReference
    {
        /// <summary>
        /// 碰撞点
        /// </summary>
        public Vector3 HitPoint;

        /// <summary>
        /// 碰撞发起者
        /// </summary>
        public object From;

        /// <summary>
        /// 被碰撞者
        /// </summary>
        public object Target;

        /// <summary>
        /// 碰撞方向
        /// </summary>
        public Vector3 HitDir;

        /// <summary>
        /// 碰撞持续时间
        /// </summary>
        public float HitDuration;

        /// <summary>
        /// 碰撞力的大小
        /// </summary>
        public float HitForce;

        /// <summary>
        /// 特效id 
        /// </summary>
        public float Effectid;

        public HitData() { }

        public static HitData Create(object from,object target,Vector3 hitpoint,Vector3 hitDir,float Duration = 0,float hitForce = 0,float effectid=0)
        {
            HitData hitData = ReferencePool.Acquire<HitData>();
            hitData.From = from;
            hitData.Target = target;
            hitData.HitPoint = hitpoint;
            hitData.HitForce = hitForce;
            hitData.HitDuration = Duration;
            hitData.HitDir = hitDir;
            hitData.Effectid = effectid;

            return hitData;
        }

        public void Clear()
        {
            From = null;
            Target = null;
            HitPoint = Vector3.zero;
        }
    }
}
