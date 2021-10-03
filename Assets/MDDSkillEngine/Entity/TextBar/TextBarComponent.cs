
using MDDGameFramework;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class TextBarComponent : MDDGameFrameworkComponent
    {
        [SerializeField]
        private TextBarItem m_TextBarItemTemplate = null;

        [SerializeField]
        private Transform m_TextBarInstanceRoot = null;

        [SerializeField]
        private int m_InstancePoolCapacity = 128;

        private IObjectPool<TextBarItemObject> m_TextBarItemObjectPool = null;
        private List<TextBarItem> m_ActiveTextBarItems = null;
        private Canvas m_CachedCanvas = null;

        private void Start()
        {
            if (m_TextBarInstanceRoot == null)
            {
                Log.Error("You must set HP bar instance root first.");
                return;
            }

            m_CachedCanvas = m_TextBarInstanceRoot.GetComponent<Canvas>();
            m_TextBarItemObjectPool = GameEnter.ObjectPool.CreateSingleSpawnObjectPool<TextBarItemObject>("TextBarPool", m_InstancePoolCapacity);
            m_ActiveTextBarItems = new List<TextBarItem>();
        }

        private void OnDestroy()
        {
        }
        private void Update()
        {
            for (int i = m_ActiveTextBarItems.Count - 1; i >= 0; i--)
            {
                TextBarItem textBarItem = m_ActiveTextBarItems[i];

                if (textBarItem.Refresh())
                {
                    continue;
                }

                HideTextBar(textBarItem);
            }
        }

        private void HideTextBar(TextBarItem textBarItem)
        {
            //textBarItem.Reset();
            m_ActiveTextBarItems.Remove(textBarItem);
            m_TextBarItemObjectPool.Unspawn(textBarItem);
        }


        public void ShowTextBar(Entity entity, int damage)
        {
            if (entity == null)
            {
                Log.Warning("Entity is invalid.");
                return;
            }

            TextBarItem textBarItem = GetActiveTextBarItem(entity);

            if (textBarItem == null)
            {
                textBarItem = CreateTextBarItem(entity);
                m_ActiveTextBarItems.Add(textBarItem);
            }

            textBarItem.Init(entity, damage, m_CachedCanvas);
        }

        private TextBarItem GetActiveTextBarItem(Entity entity)
        {
            if (entity == null)
            {
                return null;
            }

            for (int i = 0; i < m_ActiveTextBarItems.Count; i++)
            {
                if (m_ActiveTextBarItems[i].Owner == entity)
                {
                    return m_ActiveTextBarItems[i];
                }
            }

            return null;
        }

        private TextBarItem CreateTextBarItem(Entity entity)
        {
            TextBarItem textBarItem = null;
            TextBarItemObject textBarItemObject = m_TextBarItemObjectPool.Spawn();
            if (textBarItemObject != null)
            {
                textBarItem = (TextBarItem)textBarItemObject.Target;
            }
            else
            {
                textBarItem = Instantiate(m_TextBarItemTemplate);
                Transform transform = textBarItem.GetComponent<Transform>();
                transform.SetParent(m_TextBarInstanceRoot);
                transform.localScale = Vector3.one;
                m_TextBarItemObjectPool.Register(TextBarItemObject.Create(textBarItem), true);
            }

            return textBarItem;
        }
    }
}
