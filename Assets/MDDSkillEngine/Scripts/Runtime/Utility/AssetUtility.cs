﻿using MDDGameFramework;

namespace MDDSkillEngine
{
    public static class AssetUtility
    {
        public static string GetConfigAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/Configs/{0}.{1}", assetName, fromBytes ? "bytes" : "txt");
        }

        public static string GetDataTableAsset(string assetName, bool fromBytes)
        {
            if (fromBytes)
            {
                return Utility.Text.Format("Assets/MDDSkillEngine/ResourceDataTable/{0}.{1}", assetName, "bytes");
            }
            else
            {
                return Utility.Text.Format("Assets/MDDSkillEngine/DataTable/{0}.{1}", assetName, "txt");
            }

        }

        //public static string GetDictionaryAsset(string assetName, bool fromBytes)
        //{
        //    return Utility.Text.Format("Assets/GameMain/Localization/{0}/Dictionaries/{1}.{2}", GameEntrr.Localization.Language.ToString(), assetName, fromBytes ? "bytes" : "xml");
        //}

        public static string GetFontAsset(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/Fonts/{0}.otf", assetName);
        }

        /// <summary>
        /// 获取技能资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetSkillAsset(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/SkillRes/Skill/{0}.asset", assetName);
        }

        public static string GetIconAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GUIPack-Clean&Minimalist/Sprites/Icons-Demo-Small/Icons-Small-White_PNG/{0}.png", assetName);
        }

        public static string GetSkillTimelineAsset(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/SkillRes/SkillTimelineRuntime/{0}.bytes", assetName);
        }

        public static string GetSkillTimelinePrefab(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/SkillRes/SkillTimeline/{0}.prefab", assetName);
        }

        /// <summary>
        /// 获取公共黑板资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public static string GetPublicBBAsset(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/SkillRes/Skill/{0}.asset", assetName);
        }


        public static string GetSceneAsset(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/Scene/{0}.unity", assetName);
        }

        public static string GetMusicAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Music/{0}.mp3", assetName);
        }

        public static string GetSoundAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Sounds/{0}.wav", assetName);
        }

        public static string GetEntityAsset(string assetName, EntityType entityType = EntityType.Normal)
        {
            switch (entityType)
            {
                case EntityType.Effect:
                    return Utility.Text.Format("Assets/MDDSkillEngine/Prefabs/Effect/{0}.prefab", assetName);
                case EntityType.Collider:
                    return Utility.Text.Format("Assets/MDDSkillEngine/Prefabs/Collider/{0}.prefab", assetName);
                case EntityType.Hero:
                    return Utility.Text.Format("Assets/MDDSkillEngine/Prefabs/Hero/{0}.prefab", assetName);
            }

            return Utility.Text.Format("Assets/MDDSkillEngine/Prefabs/{0}.prefab", assetName);
        }

        public static string GetEffectAsset(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/Prefabs/Effect/{0}.prefab", assetName);
        }

        public static string GetColliderAsset(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/Prefabs/Collider/{0}.prefab", assetName);
        }

        public static string GetUIFormAsset(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/UI/{0}.prefab", assetName);
        }

        public static string GetUISoundAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/UISounds/{0}.wav", assetName);
        }
    }
}
