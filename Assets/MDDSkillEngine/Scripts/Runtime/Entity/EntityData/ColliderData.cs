﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class ColliderData : EntityData
    {
        private Entity m_Owner;

        private float m_duration = 999;


        private int m_EffectID = 0;

        private bool m_IsFollowParent=true;

        public string buffName="";

        public Vector3 boundSize;

        public Vector3 boundCenter;

        public Vector3 localeftPostion;

        public Quaternion localRotation;

        public Vector3 localScale;


        public Entity Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public float Duration
        {
            get
            {
                return m_duration;
            }
            set
            {
                m_duration = value;
            }
        }



        public int EffectID
        {
            get 
            { 
                return m_EffectID; 
            }
            set 
            { 
                m_EffectID = value; 
            }
        }

        public bool IsFollowParent
        {
            get { return m_IsFollowParent; }
            set { m_IsFollowParent = value;}
        }




        public ColliderData(int entityId, int typeId, Entity owner)
           : base(entityId, typeId)
        {
            m_Owner = owner;
            EntityType = EntityType.Collider;
        }
    }

}

