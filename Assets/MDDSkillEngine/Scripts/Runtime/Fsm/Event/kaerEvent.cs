using MDDGameFramework;


namespace MDDSkillEngine
{
    public class kaerNormalAttackEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(kaerNormalAttackEventArgs).GetHashCode();

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

        public static kaerNormalAttackEventArgs Create(Entity entity)
        {
            kaerNormalAttackEventArgs e = ReferencePool.Acquire<kaerNormalAttackEventArgs>();
            e.entity = entity;
            return e;
        }

        public override void Clear()
        {
            entity = null;
        }
    }

}
