using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public class chaoxidazhao : MonoBehaviour
    {



        // Start is called before the first frame update
        void Start()
        {
            CreateCircleRayCast(20, 10);
        }

        // Update is called once per frame
        void Update()
        {
            if (Physics.Raycast(Vector3.zero, Vector3.up, 100, 1 << 0))
            {
                //GameObject go = cameraHit.transform.gameObject; //这是检测到的物体
            }
        }


        /// <summary>
        /// 圆弧创建射线检测射线检测方法1
        /// </summary>
        /// <param name="radius">半径</param>
        /// <param name="angle">角度</param>
        void CreateCircleRayCast(float radius, float angle)
        {
            //循环创建cube
            for (int i = 0; i < 36; i++, angle += 10 )
            {             
                float x = radius * Mathf.Cos(angle * Mathf.PI / 180f);
                float z = radius * Mathf.Sin(angle * Mathf.PI / 180f);
                //创建cube并设置新cube的position
                GameObject.CreatePrimitive(PrimitiveType.Cube).transform.position = new Vector3(x, 0 , z);
            }
        }

    }
}
