using MDDGameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDSkillEngine
{
    public class ReleaseSkillEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(ReleaseSkillEventArgs).GetHashCode();

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

        public static ReleaseSkillEventArgs Create(ISkillSystem SkillSystem,int SkillID)
        {
            ReleaseSkillEventArgs releaseSkillEventArgs = ReferencePool.Acquire<ReleaseSkillEventArgs>();
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

