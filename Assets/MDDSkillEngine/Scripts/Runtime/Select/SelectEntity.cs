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
        /// <summary>
        /// 寻路点
        /// </summary>
        public GameObject pathFindingTarget;

        /// <summary>
        /// 鼠标放置位置
        /// </summary>
        public GameObject mouseTarget;


        /// <summary>
        /// 当前攻击目标
        /// </summary>
        public Entity attackTarget;


        /// <summary>
        /// 框选实体组件
        /// </summary>
        public List<Entity> entities = new List<Entity>();

        /// <summary>
        /// 鼠标左键选择的实体
        /// 现阶段激活的实体
        /// </summary>
        public Entity selectEntity;

        /// <summary>
        /// 鼠标悬停的实体
        /// </summary>      
        public Entity mouseSeeEntity;


        public List<Entity> highLightEntity = new List<Entity>();


        public Vector3 currentMouse;


        public bool isWork;


        public void InitState()
        {
            Game.Input.Control.Heros_Normal.LeftClick.performed += OnClickLeft;
            Game.Input.Control.Heros_Normal.RightClick.performed += OnClickRight;
        }

        private void FixedUpdate()
        {
            if (isWork)
            {
                if (SelectUtility.MouseRayCastByLayer(1 << 8 | 1 << 11 | 1 << 0 | 1 << 1, out RaycastHit hit))
                {
                    MDDGameFramework.Runtime.Entity entity = hit.transform.GetComponent<MDDGameFramework.Runtime.Entity>();

                    if (entity != null)
                    {
                        attackTarget = entity.Logic as Entity;
                    }
                    else
                    {
                        attackTarget = null;
                    }
                }
            }         
        }

        private void OnClickLeft(CallbackContext ctx)
        {
            if (!isWork)
                return;

            if (SelectUtility.MouseRayCastByLayer(1 << 8 | 1 << 11, out RaycastHit hit))
            {
                MDDGameFramework.Runtime.Entity entity = hit.transform.GetComponent<MDDGameFramework.Runtime.Entity>();

                if (entity != null)
                {
                    Game.Event.Fire(this, SelectEntityEventArgs.Create(entity.Logic as Entity));
                    selectEntity = entity.Logic as Entity;
                }
            }
        }

        private void OnClickRight(CallbackContext ctx)
        {
            if (!isWork)
                return;

            if (SelectUtility.MouseRayCastByLayer(1 << 8 | 1 << 11, out RaycastHit hit))
            {
                MDDGameFramework.Runtime.Entity entity = hit.transform.GetComponent<MDDGameFramework.Runtime.Entity>();

                if (entity != null)
                {
                    Game.Event.Fire(this, SelectAttackEntityEventArgs.Create(entity.Logic as Entity));
                    attackTarget = entity.Logic as Entity;
                    return;
                }
            }

            if (SelectUtility.MouseRayCastByLayer(1 << 0 | 1 << 1, out RaycastHit vector3))
            {
                pathFindingTarget.transform.position = vector3.point;
                selectEntity.CacheMove.SearchPath();
                selectEntity.SetState(EntityNormalState.RUN, true);
                Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70000) { Position = vector3.point });
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


