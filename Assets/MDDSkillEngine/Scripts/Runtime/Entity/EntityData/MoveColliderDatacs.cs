using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace MDDSkillEngine
{
    public class MoveColliderData : ColliderData
    {
        /// <summary>
        /// 移动速度
        /// </summary>
        public float Speed;

        /// <summary>
        /// 移动方向
        /// </summary>
        public Vector3 Dir;

        public MoveColliderData(int entityId, int typeId, Entity owner) : base(entityId, typeId, owner)
        {

        }
    }
}