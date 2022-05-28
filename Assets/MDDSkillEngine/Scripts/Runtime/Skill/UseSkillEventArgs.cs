using MDDGameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDSkillEngine
{
    public class UseSkillEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(UseSkillEventArgs).GetHashCode();

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

        public static UseSkillEventArgs Create(ISkillSystem SkillSystem, int SkillID)
        {
            UseSkillEventArgs releaseSkillEventArgs = ReferencePool.Acquire<UseSkillEventArgs>();
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

