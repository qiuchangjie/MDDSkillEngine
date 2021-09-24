﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class SelectEntity : MonoBehaviour
    {
        public List<Entity> entities;
        public static Entity selectEntity;

        public static void InitSelectEntity(Entity entity)
        {
            if(entity!=null)
            {
                selectEntity = entity;

                Log.Error("entityname:{0},entityid{1}", entity.name, entity.Id);
            }
        }

        public static void ClearSelectEntity(IEntity entity)
        {
            selectEntity = null;
        }

    }
}


