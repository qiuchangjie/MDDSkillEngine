#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;

namespace MDDGameFramework
{
    public static class NPBlackBoardEditorInstance 
    {
        public static Dictionary<string, Variable> BBValues = new Dictionary<string, Variable>();

        public static Dictionary<string, Variable> AllBB = new Dictionary<string, Variable>();

        public static List<string> buffs = new List<string>();

        public static List<string> Effects = new List<string>();

        public static List<string> Collider = new List<string>();

        public static List<string> ColliderLogic = new List<string>();

        public static List<string> EffectLogic = new List<string>();

        public static List<string> SkillTimelines = new List<string>();

        public static List<string> Hero = new List<string>();

        public static List<string> HeroLogic = new List<string>();
    }

}

#endif