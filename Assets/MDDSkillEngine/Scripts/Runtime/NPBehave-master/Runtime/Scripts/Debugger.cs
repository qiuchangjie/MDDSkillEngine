using UnityEngine;
using System.Collections.Generic;
using MDDGameFramework;
using MDDGameFramework.Runtime;


namespace MDDSkillEngine
{
    public class Debugger : MonoBehaviour
    {
        public Root BehaviorTree;

        private static Blackboard _customGlobalStats = null;
        public static Blackboard CustomGlobalStats
        {
            get
            {
                if (_customGlobalStats == null)
                {
                    _customGlobalStats = UnityContext.GetSharedBlackboard("_GlobalStats"); ;
                }
                return _customGlobalStats;
            }
        }

        private Blackboard _customStats = null;
        public Blackboard CustomStats
        {
            get
            {
                if (_customStats == null)
                {
                    _customStats = new Blackboard(CustomGlobalStats, UnityContext.GetClock());
                }
                return _customStats;
            }
        }

        private VarInt32 m_int;


        //public void DebugCounterInc(string key)
        //{
        //    if (!CustomStats.Isset(key))
        //    {
        //        m_int = 0;
        //        CustomStats[key] = m_int;
        //    }
        //    CustomStats[key] = CustomStats.Get<VarInt32>(key) + 1;
        //}

        //public void DebugCounterDec(string key)
        //{
        //    if (!CustomStats.Isset(key))
        //    {
        //        CustomStats[key] = 0;
        //    }
        //    CustomStats[key] = CustomStats.Get<int>(key) - 1;
        //}

        //public static void GlobalDebugCounterInc(string key)
        //{
        //    if (!CustomGlobalStats.Isset(key))
        //    {
        //        CustomGlobalStats[key] = 0;
        //    }
        //    CustomGlobalStats[key] = CustomGlobalStats.Get<int>(key) + 1;
        //}

        //public static void GlobalDebugCounterDec(string key)
        //{
        //    if (!CustomGlobalStats.Isset(key))
        //    {
        //        CustomGlobalStats[key] = 0;
        //    }
        //    CustomGlobalStats[key] = CustomGlobalStats.Get<int>(key) - 1;
        //}

    }

}

