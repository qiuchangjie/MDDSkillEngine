using MDDGameFramework;

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
                return Utility.Text.Format("Assets/MDDSkillEngine/DataTable/{0}.{1}", assetName,"txt");
            }

        }

        //public static string GetDictionaryAsset(string assetName, bool fromBytes)
        //{
        //    return Utility.Text.Format("Assets/GameMain/Localization/{0}/Dictionaries/{1}.{2}", GameEntrr.Localization.Language.ToString(), assetName, fromBytes ? "bytes" : "xml");
        //}

        public static string GetFontAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/Fonts/{0}.ttf", assetName);
        }

        public static string GetSkillAsset(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/SkillData/{0}.asset", assetName);
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

        public static string GetEntityAsset(string assetName)
        {
            return Utility.Text.Format("Assets/MDDSkillEngine/Prefabs/{0}.prefab", assetName);
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
