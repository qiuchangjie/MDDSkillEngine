using MDDGameFramework;
using MDDGameFramework.Runtime;
using UnityEngine;

namespace MDDSkillEngine
{
    public static class SelectUtility
    {
        public static bool IsInsert(Vector3 start, Vector3 end, Vector3 point)
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



        /// <summary>
        /// 世界坐标转换为ui坐标
        /// </summary>
        /// <param name="worldPosition">世界坐标</param>
        /// <param name="rectTransform">依托的canvans</param>
        /// <returns></returns>
        public static Vector2 WorldToUgui(Vector3 worldPosition, RectTransform rectTransform)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Game.Scene.MainCamera, worldPosition);

            Vector2 localPoint;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, Game.Scene.UICamera, out localPoint);

            return localPoint;
        }


        /// <summary>
        /// 返回鼠标射线检测坐标
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static bool MouseRayCastByLayer(int layer,ref Vector3 vector3)
        {
            RaycastHit hit;
            if (Physics.Raycast(Game.Scene.MainCamera.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, layer))
            {
                vector3 = hit.point;

                return true;
            }
            else
            {
                Log.Info("射线射空了------");
                return false;
            }
        
        }

    }
}
