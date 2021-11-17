

using System.Collections;
using UnityEngine;
using MDDGameFramework;
using UnityEngine.UI;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class HPBarItem : MonoBehaviour
    {
        private const float AnimationSeconds = 0.3f;
        private const float KeepSeconds = 0.4f;
        private const float FadeOutSeconds = 0.3f;

        [SerializeField]
        private Slider m_HPBar = null;

        private Canvas m_ParentCanvas = null;
        private RectTransform m_CachedTransform = null;
        private CanvasGroup m_CachedCanvasGroup = null;
        private Entity m_Owner = null;
        private int m_OwnerId = 0;

        private Coroutine hpcoroutine = null;

        private float toHPRatio;

        public Entity Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public void HpChange(float toHPRotio)
        {
            if (hpcoroutine != null)
            {
                StopCoroutine(hpcoroutine);
            }

            this.toHPRatio = toHPRotio;

            hpcoroutine = StartCoroutine(HPBarCo(this.toHPRatio, AnimationSeconds));
        }

        public void Init(Entity owner, Canvas parentCanvas, float fromHPRatio, float toHPRatio)
        {
            if (owner == null)
            {
                Log.Error("Owner is invalid.");
                return;
            }

            m_ParentCanvas = parentCanvas;

            gameObject.SetActive(true);

            if (hpcoroutine != null)
            {
                StopCoroutine(hpcoroutine);
            }
            //StopAllCoroutines();

            m_CachedCanvasGroup.alpha = 1f;

            if (m_Owner != owner || m_OwnerId != owner.Id)
            {
                m_HPBar.value = fromHPRatio;
                m_Owner = owner;
                m_OwnerId = owner.Id;
            }

            Refresh();

            this.toHPRatio = toHPRatio;

            hpcoroutine = StartCoroutine(HPBarCo(this.toHPRatio, AnimationSeconds));
        }

        public bool Refresh()
        {
            if (m_CachedCanvasGroup.alpha <= 0f)
            {
                return false;
            }

            if (m_Owner != null && Owner.Available && Owner.Id == m_OwnerId)
            {
                Vector3 worldPosition = m_Owner.CachedTransform.position;//+ new Vector3(0, 1.8f ,0);

                m_CachedTransform.localPosition = SelectUtility.WorldToUgui(worldPosition, (RectTransform)m_ParentCanvas.transform);
            }

            return true;
        }

        public void Reset()
        {
            StopAllCoroutines();
            m_CachedCanvasGroup.alpha = 1f;
            m_HPBar.value = 1f;
            m_Owner = null;
            gameObject.SetActive(false);
        }

        private void Awake()
        {
            m_CachedTransform = GetComponent<RectTransform>();
            if (m_CachedTransform == null)
            {
                Log.Error("RectTransform is invalid.");
                return;
            }

            m_CachedCanvasGroup = GetComponent<CanvasGroup>();
            if (m_CachedCanvasGroup == null)
            {
                Log.Error("CanvasGroup is invalid.");
                return;
            }
        }

        private IEnumerator HPBarCo(float value, float animationDuration)
        {
            yield return m_HPBar.SmoothValue(value, animationDuration);
            //yield return new WaitForSeconds(keepDuration);
            //yield return m_CachedCanvasGroup.FadeToAlpha(0f, fadeOutDuration);
        }
    }
}
