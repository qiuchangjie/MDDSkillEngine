using MDDGameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDSkillEngine
{
    public class RemoveSkillEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(RemoveSkillEventArgs).GetHashCode();

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

        public static RemoveSkillEventArgs Create(ISkillSystem SkillSystem, int SkillID)
        {
            RemoveSkillEventArgs releaseSkillEventArgs = ReferencePool.Acquire<RemoveSkillEventArgs>();
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

