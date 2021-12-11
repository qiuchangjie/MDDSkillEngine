using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class SelectEntity : MDDGameFrameworkComponent
    {
        public GameObject pathFindingTarget;

        public static List<Entity> entities=new List<Entity>();
        public static Entity selectEntity;
        public static Entity Player;

        public Vector3 currentClick;

        public void InitPlayer(Entity entity)
        {
            if (entity != null)
            {
                Player = entity;
            }
        }
       
        public void InitSelectEntity(Entity entity)
        {
            if(entity!=null)
            {
                selectEntity = entity;
                if (entity.gameObject.layer == 11)
                {
                    entities.Add(entity);
                }
                //Log.Error("entityname:{0},entityid{1}", entity.name, entity.Id);
            }
        }

        public void ClearSelectEntity(IEntity entity)
        {
            selectEntity = null;
        }

        private void Update()
        {

            
            RaycastHit hit;
            if (Physics.Raycast(Game.Scene.MainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1 << 8))
            {              
                Entity e = hit.collider.gameObject.GetComponent<Entity>();
                if (e != null)
                {
                    e.CacheOutLiner.SetOutLiner(true);
                }            
            }
        }

    }
}


