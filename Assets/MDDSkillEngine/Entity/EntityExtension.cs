using System;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int s_SerialId = 0;

        public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            MDDGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (Entity)entity.Logic;
        }

        public static void fucktestaction(this GameObject gameobject, BulletData test)
        {

        }

        public static void HideEntity(this EntityComponent entityComponent, Entity entity)
        {
            entityComponent.HideEntity(entity.Entity);
        }

        public static void AttachEntity(this EntityComponent entityComponent, Entity entity, int ownerId, string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity.Entity, ownerId, parentTransformPath, userData);
        }

       

        public static void ShowBullet(this EntityComponent entityCompoennt, BulletData data)
        {
            entityCompoennt.ShowEntity(typeof(Bullet),"Bullet", Constant.AssetPriority.BulletAsset, data);
        }

        public static void ShowPlayer(this EntityComponent entityCompoennt, BulletData data)
        {
            entityCompoennt.ShowEntity(typeof(Player), "Player", Constant.AssetPriority.AircraftAsset, data);
        }

        public static void ShowEffect(this EntityComponent entityComponent, EffectData data)
        {
            entityComponent.ShowEntity(typeof(Effect), "Effect", Constant.AssetPriority.EffectAsset, data);
        }

        private static void ShowEntity(this EntityComponent entityComponent, Type logicType, string entityGroup, int priority, EntityData data,string AssetName=null)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            //IDataTable<DREntity> dtEntity = GameEntry.DataTable.GetDataTable<DREntity>();
            //DREntity drEntity = dtEntity.GetDataRow(data.TypeId);
            //if (drEntity == null)
            //{
            //    Log.Warning("Can not load entity id '{0}' from data table.", data.TypeId.ToString());
            //    return;
            //}

            if (data.name != "")
            {
                AssetName = data.name;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(AssetName), entityGroup, priority, data);
        }

        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}
