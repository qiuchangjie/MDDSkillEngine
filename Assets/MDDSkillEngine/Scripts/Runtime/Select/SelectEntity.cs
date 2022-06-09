using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.InputSystem;

namespace MDDSkillEngine
{
    public class SelectEntity : MDDGameFrameworkComponent
    {
        public GameObject pathFindingTarget;

        public GameObject mouseTarget;

        public List<Entity> entities = new List<Entity>();
        /// <summary>
        /// 鼠标左键选择的实体
        /// </summary>
        public Entity selectEntity;
        /// <summary>
        /// 鼠标悬停的实体
        /// </summary>
        public Entity mouseSeeEntity;
        public Entity Player;

        public List<Entity> highLightEntity = new List<Entity>();

        public Vector3 currentMouse;

        public bool isWork;


        public void InitState()
        {
            Game.Input.Control.Heros_Normal.LeftClick.performed += OnClickLeft;
        }


        private void OnClickLeft(CallbackContext ctx)
        {
            if (SelectUtility.MouseRayCastByLayer(1 << 8 | 1 << 11, out RaycastHit hit))
            {
                MDDGameFramework.Runtime.Entity entity = hit.transform.GetComponent<MDDGameFramework.Runtime.Entity>();

                if (entity != null)
                {
                    Game.Event.Fire(this,SelectEntityEventArgs.Create(entity.Logic as Entity));
                    selectEntity = entity.Logic as Entity; 
                }
            }                    
        }

        public void InitPlayer(Entity entity)
        {
            if (entity != null)
            {
                Game.Event.Fire(this, SelectEntityEventArgs.Create(entity));
                selectEntity = entity;
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

        private void AddHighLight(Entity e)
        {
            if (!highLightEntity.Contains(e))
            {
                highLightEntity.Add(e);
            }
        }

    }
}


