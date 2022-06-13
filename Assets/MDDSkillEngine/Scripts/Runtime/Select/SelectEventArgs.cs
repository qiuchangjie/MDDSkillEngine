using MDDGameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MDDSkillEngine
{
    public class SelectEntityEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(SelectEntityEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public Entity entity
        {
            get;
            private set;
        }

        public static SelectEntityEventArgs Create(Entity entity)
        {
            SelectEntityEventArgs e = ReferencePool.Acquire<SelectEntityEventArgs>();
            e.entity = entity;
            return e;
        }

        public override void Clear()
        {
            entity = null;
        }
    }

    public class SelectAttackEntityEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(SelectAttackEntityEventArgs).GetHashCode();

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public Entity entity
        {
            get;
            private set;
        }

        public static SelectAttackEntityEventArgs Create(Entity entity)
        {
            SelectAttackEntityEventArgs e = ReferencePool.Acquire<SelectAttackEntityEventArgs>();
            e.entity = entity;
            return e;
        }

        public override void Clear()
        {
            entity = null;
        }
    }

}

