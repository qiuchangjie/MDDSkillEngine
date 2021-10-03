using MDDGameFramework;
using UnityEngine;

namespace MDDSkillEngine
{
    public class TextBarItemObject : ObjectBase
    {
        public static TextBarItemObject Create(object target)
        {
            TextBarItemObject textBarItemObject = ReferencePool.Acquire<TextBarItemObject>();
            textBarItemObject.Initialize(target);
            return textBarItemObject;
        }

        protected override void Release(bool isShutdown)
        {
            TextBarItem textBarItem = (TextBarItem)Target;
            if (textBarItem == null)
            {
                return;
            }

            Object.Destroy(textBarItem.gameObject);
        }
    }
}
