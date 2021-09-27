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
    }
}
