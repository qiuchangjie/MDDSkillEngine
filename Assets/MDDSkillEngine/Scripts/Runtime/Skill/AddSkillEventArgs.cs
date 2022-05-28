using MDDGameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDSkillEngine
{
    public class AddSkillEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(AddSkillEventArgs).GetHashCode();

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

        public int Index
        {
            get;
            private set;
        }

        public static AddSkillEventArgs Create(ISkillSystem SkillSystem, int SkillID,int Index)
        {
            AddSkillEventArgs e = ReferencePool.Acquire<AddSkillEventArgs>();
            e.SkillSystem = SkillSystem;
            e.SkillID = SkillID;

            return e;
        }



        public override void Clear()
        {
            SkillID = 0;
            SkillSystem = null;
        }
    }

}

