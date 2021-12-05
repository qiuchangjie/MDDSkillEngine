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

        public int Id
        {
            get
            {
                return Entity.Id;
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

        public AnimationContainer CachedAnimContainer
        {
            get;
            private set;
        }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            CachedAnimator = GetComponent<Animator>();
            CachedAnimancer = GetComponent<AnimancerComponent>();
            CachedAnimContainer = GetComponent<AnimationContainer>();
        }


        protected override void OnRecycle()
        {
            base.OnRecycle();
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

            if (m_EntityData.IsPreLoad)
            {
                HideSelf();
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
        }

        protected virtual void HideSelf()
        {
            Game.Entity.HideEntity(Id);
        }
    }
}
