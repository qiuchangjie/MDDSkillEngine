using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class TextBarItem : MonoBehaviour
    {

        /// <summary>
        /// textbar拥有者 
        /// </summary>
        private Entity m_Owner;

        /// <summary>
        /// 伤害文本
        /// </summary>
        [SerializeField]
        private Text damage;

        private int m_OwnerId = 0;
        private Canvas m_ParentCanvas;
        private RectTransform m_CachedTransform = null;

        RectTransform rectTransform;



        public Entity Owner
        {
            get
            {
                return m_Owner;
            }
        }

        public RectTransform RectTransform
        {
            get
            {
                return rectTransform;
            }
        }


        private void Awake()
        {
            m_CachedTransform = GetComponent<RectTransform>();
            if (m_CachedTransform == null)
            {
                Log.Error("RectTransform is invalid.");
                return;
            }

         
        }

        public void Init(Entity owner,int damage, Canvas parentCanvas)
        {
            if (owner == null)
            {
                Log.Error("Owner is invalid.");
                return;
            }

            gameObject.SetActive(true);

            m_ParentCanvas = parentCanvas;

            if (m_Owner != owner||m_OwnerId!=owner.Id)
            {               
                m_Owner = owner;
                m_OwnerId = owner.Id;
            }

          
            this.damage.text = Utility.Text.Format("-{0}", damage);

            Refresh();

            m_CachedTransform.DOLocalMoveY(m_CachedTransform.localPosition.y+30f, 0.8f).OnComplete(()=> { gameObject.SetActive(false); });
        }

        public bool Refresh()
        {
            if (!gameObject.activeSelf)
            {
                return false;
            }

            if (m_Owner != null && Owner.Available && Owner.Id == m_OwnerId)
            {
                Vector3 worldPosition = m_Owner.CachedTransform.position + new Vector3(0, 1.8f, 0);
                //Vector3 screenPosition = GameEnter.Scene.MainCamera.WorldToScreenPoint(worldPosition);
                Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

                Vector2 position;
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)m_ParentCanvas.transform, screenPosition,
                    m_ParentCanvas.worldCamera, out position))
                {
                    m_CachedTransform.localPosition = position;
                }
            }

            return true;
        }

    }
}
