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

        public List<Entity> entities = new List<Entity>();
        public Entity selectEntity;
        public Entity Player;

        public List<Entity> highLightEntity = new List<Entity>();

        public Vector3 currentClick;

        public bool isWork;

        public void InitPlayer(Entity entity)
        {
            if (entity != null)
            {
                Player = entity;
            }
        }

        public void InitSelectEntity(Entity entity)
        {
            if (entity != null)
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
            if (!isWork)
                return;

            //RaycastHit hit;
            //if (Physics.Raycast(Game.Scene.MainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, 1 << 8))
            //{
            //    Entity e = hit.collider.gameObject.GetComponent<Entity>();
            //    if (e != null)
            //    {
            //        if (e.SelectState != EntitySelectState.OnSelect)
            //        {
            //            e.SwitchEntitySelectState(EntitySelectState.OnHighlight);
            //        }
            //    }
            //}
        }

        private void AddHighLight(Entity e)
        {
            if (!highLightEntity.Contains(e))
            {
                highLightEntity.Add(e);
            }
        }

    }
}


