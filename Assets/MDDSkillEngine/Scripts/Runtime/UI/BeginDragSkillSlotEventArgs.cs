using MDDGameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDSkillEngine
{
    public class BeginDragSkillSlotEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(BeginDragSkillSlotEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public int SkillID
        {
            get;
            private set;
        }

        public string SkillName
        {
            get;
            private set;
        }

        public static BeginDragSkillSlotEventArgs Create(int id,string SkillName)
        {
            BeginDragSkillSlotEventArgs e = ReferencePool.Acquire<BeginDragSkillSlotEventArgs>();
            e.SkillID = id;
            e.SkillName = SkillName;

            return e;
        }

        public override void Clear()
        {
            SkillID = 0;
            SkillName = "";
        }
    }

    public class DragSkillSlotEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(DragSkillSlotEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

     

        public static DragSkillSlotEventArgs Create()
        {
            DragSkillSlotEventArgs e = ReferencePool.Acquire<DragSkillSlotEventArgs>();
          
            return e;
        }

        public override void Clear()
        {
          
        }
    }

    public class EndDragSkillSlotEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(EndDragSkillSlotEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public bool isReycast
        {
            get;
            private set;
        }

        public AblitiesSlot ablitiesSlot
        {
            get;
            private set;
        }

        public int SkillID
        {
            get;
            private set;
        }


        public static EndDragSkillSlotEventArgs Create(bool isReycast, AblitiesSlot ablitiesSlot = null, int skillid = 0)
        {
            EndDragSkillSlotEventArgs e = ReferencePool.Acquire<EndDragSkillSlotEventArgs>();
            e.isReycast = isReycast;
            e.ablitiesSlot = ablitiesSlot;
            e.SkillID = skillid;

            return e;
        }

        public override void Clear()
        {

        }
    }

}

