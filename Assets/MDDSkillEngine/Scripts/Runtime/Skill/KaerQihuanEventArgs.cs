using MDDGameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDSkillEngine
{
    public class KaerQihuanEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(KaerQihuanEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public ISkillSystem SkillSystem
        {
            get;
            private set;
        }

        public int SkillID
        {
            get;
            private set;
        }

        public static KaerQihuanEventArgs Create(ISkillSystem SkillSystem, int SkillID)
        {
            KaerQihuanEventArgs releaseSkillEventArgs = ReferencePool.Acquire<KaerQihuanEventArgs>();
            releaseSkillEventArgs.SkillSystem = SkillSystem;
            releaseSkillEventArgs.SkillID = SkillID;

            return releaseSkillEventArgs;
        }

        public override void Clear()
        {
            SkillID = 0;
            SkillSystem = null;
        }
    }

}

