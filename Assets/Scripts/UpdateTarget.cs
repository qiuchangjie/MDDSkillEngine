using Animancer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MDDGameFramework;
using MDDSkillEngine;

namespace Pathfinding
{
    public class UpdateTarget : MonoBehaviour
    {
        IBuffSystem buffSystem;

        public Transform target;

        public GameObject clickPrefab;

        public LayerMask mask;
        IAstarAI[] ais;
        Camera main;

        public AnimancerComponent animancer;

        private void Awake()
        {          
            main = GetComponent<Camera>();
            ais = FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray();
        }

        private void Start()
        {
            buffSystem = GameEnter.Buff.CreatBuffSystem();
            entiity entiity = GameObject.Find("GameObject").GetComponent<entiity>();
            buffSystem.AddBuff(1,entiity.obj[0],entiity.obj[1]);
        }

        bool isClick = false;
        bool isAttact = false;

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

            if (Input.GetKeyDown(KeyCode.X))
            {
                isAttact = true;
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

                    PathFindingTest.workAction.Invoke();


                    for (int i = 0; i < ais.Length; i++)
                    {
                        ais[i].SearchPath();
                    }

                    //positionFound = true;
                }
                isClick = false;
            }

            if (isAttact)
            {
                PathFindingTest.attackAction.Invoke();
                isAttact = false;
            }

           
        }


    }

}

