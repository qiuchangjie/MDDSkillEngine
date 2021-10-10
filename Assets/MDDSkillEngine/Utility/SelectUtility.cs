using MDDGameFramework;
using UnityEngine;

namespace MDDSkillEngine
{
    public static class SelectUtility
    {
        public static bool IsInsert(Vector3 start,Vector3 end,Vector3 point)
        {
            bool isIn = false;
            bool isInx = false;

            if (start.x >= end.x)
            {
                if (point.x < start.x && point.x >= end.x)
                {
                    isInx = true;
                }
                else
                    isInx = false;
            }
            else
            {
                if (point.x > start.x && point.x <= end.x)
                {
                    isInx = true;
                }
                else
                    isInx = false;
            }


            if (start.y >= end.y)
            {
                if (point.y < start.y && point.y >= end.y)
                {
                    isIn = true;
                }
                else
                    isIn = false;
            }
            else
            {
                if (point.y > start.y && point.y <= end.y)
                {
                    isIn = true;
                }
                else
                    isIn = false;
            }

            return isIn && isInx;
        }



        // 将世界坐标转换为Ugui坐标
        public static Vector2 WorldToUgui(Vector3 position,RectTransform rectTransform)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(position);//世界坐标转换为屏幕坐标
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            //screenPoint += screenSize / 2;//将屏幕坐标变换为以屏幕中心为原点
            Vector2 anchorPos = screenPoint / screenSize * rectTransform.sizeDelta;//缩放得到UGUI坐标
            return anchorPos;
        }

    }
}
