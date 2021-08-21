using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Pathfinding
{
    public class UpdateTarget : MonoBehaviour
    {
        public Transform target;

        public GameObject clickPrefab;

        public LayerMask mask;
        IAstarAI[] ais;
        Camera main;

        private void Awake()
        {
            main = GetComponent<Camera>();
            ais = FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray();
        }

        bool isClick = false;

        public void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                Debug.LogError("点击点击点击点击点击点击");
                isClick = true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.LogError("点击点击点击点击点击点击");
                isClick = true;
            }
        }

        public void LateUpdate()
        {
            if (isClick)
            {
                RaycastHit hit;
                if (Physics.Raycast(main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, mask))
                {
                    target.position = hit.point;

                    GameObject obj = Instantiate(clickPrefab);
                    obj.transform.position = hit.point;
                    

                    for (int i = 0; i < ais.Length; i++)
                    {
                        ais[i].SearchPath();
                    }

                    //positionFound = true;
                }
                isClick = false;
            }

           
        }


    }

}

