using MDDGameFramework;
using UnityEngine;
using MDDGameFramework.Runtime;
using Animancer;

namespace MDDSkillEngine
{
    public abstract class Entity : EntityLogic
    {
        [SerializeField]
        private EntityData m_EntityData = null;

        public float wasDuration;

        /// <summary>
        /// 实体黑板
        /// </summary>
        public Blackboard blackboard;
        public Clock clock;

        public System.Action<Blackboard.Type,Variable> CacheAction;

        public int Id
        {
            get
            {
                return Entity.Id;
            }
        }

        private EntitySelectState m_SelectState = EntitySelectState.None;

        public EntitySelectState SelectState
        {
            get
            {
                return m_SelectState;
            }
        }

        public Animator CachedAnimator
        {
            get;
            private set;
        }

        public AnimancerComponent CachedAnimancer
        {
            get;
            private set;
        }

        public Rigidbody Rigidbody
        {
            get;
            private set;
        }

        public AnimationContainer CachedAnimContainer
        {
            get;
            private set;
        }

        public OutLinerList CacheOutLiner
        {
            get;
            private set;
        }

        public PlayerMovement CacheMove
        {
            get;
            private set;
        }


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            clock = new Clock();
            CachedAnimator = GetComponent<Animator>();
            CachedAnimancer = GetComponent<AnimancerComponent>();
            CachedAnimContainer = GetComponent<AnimationContainer>();
            CacheOutLiner = GetComponent<OutLinerList>();
            Rigidbody = GetComponent<Rigidbody>();
            CacheMove = GetComponent<PlayerMovement>();

            blackboard = Blackboard.Create(clock);

            blackboard.Set<VarFloat>("PlayableSpeed", 1);

            CacheAction = ObservingPlayableSpeed;
        }


        protected override void OnRecycle()
        {
            base.OnRecycle();

            blackboard.RemoveObserver("PlayableSpeed", CacheAction);
        }


        protected override void OnShow(object userData)
        {
            base.OnShow(userData);

            m_EntityData = userData as EntityData;
            if (m_EntityData == null)
            {
                Log.Error("Entity data is invalid.");
                return;
            }

            Name = Utility.Text.Format("[Entity {0}]", Id.ToString());
            CachedTransform.localPosition = m_EntityData.Position;
            CachedTransform.localRotation = m_EntityData.Rotation;
            CachedTransform.localScale = m_EntityData.LocalScale;

            blackboard.AddObserver("PlayableSpeed",CacheAction);

            wasDuration = 0;

            if (m_EntityData.IsPreLoad)
            {
                Game.Coroutine.StartJobDelayed(0.1f).Start((b) =>
                {
                    HideSelf();
                });
            }
        }


        protected override void OnHide(bool isShutdown, object userData)
        {
            base.OnHide(isShutdown, userData);
        }

        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
        {
            base.OnAttached(childEntity, parentTransform, userData);
        }


        protected override void OnDetached(EntityLogic childEntity, object userData)
        {
            base.OnDetached(childEntity, userData);
        }

        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)

        {
            base.OnAttachTo(parentEntity, parentTransform, userData);

        }


        protected override void OnDetachFrom(EntityLogic parentEntity, object userData)
        {
            base.OnDetachFrom(parentEntity, userData);
        }

        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)

        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            wasDuration += elapseSeconds;
            clock.Update(elapseSeconds);
        }

        protected virtual void HideSelf()
        {
            Game.Entity.HideEntity(Id);
        }

        public virtual void SetState(EntityNormalState state, bool b)
        {

        }

        public virtual void ObservingPlayableSpeed(Blackboard.Type type, Variable newValue)
        {
            if (type == Blackboard.Type.CHANGE)
            {
                VarFloat varFloat = (VarFloat)newValue;
                if (CachedAnimancer != null)
                {
                    CachedAnimancer.Playable.Speed = varFloat.Value;
                }

                if (CacheMove != null)
                {
                    CacheMove.speed = varFloat.Value;
                }
            }           
        }

        public void SwitchEntitySelectState(EntitySelectState state)
        {
            if (!m_EntityData.IsCanSelect)
                return;

            m_SelectState = state;
        }
    }

    public enum EntitySelectState
    {
        None,

        OnSelect,

        OnHighlight,
    }

    public enum EntityNormalState
    {
        NONE,
        /// <summary>
        /// 待机
        /// </summary>
        IDLE,
        /// <summary>
        /// 移动
        /// </summary>
        RUN,
        /// <summary>
        /// 被吹飞
        /// </summary>
        FLYSKY,
        /// <summary>
        /// 被击中
        /// </summary>
        ATTACKED,
        /// <summary>
        /// 被抓取 眩晕
        /// </summary>
        CONTROLLED,

        /// <summary>
        /// 太空漫步
        /// </summary>
        SPACEWALK,

    }
}
