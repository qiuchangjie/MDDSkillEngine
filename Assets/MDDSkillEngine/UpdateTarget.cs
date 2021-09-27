﻿using Animancer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using MDDGameFramework;
using CatAsset;

namespace MDDSkillEngine
{
    public class UpdateTarget : MonoBehaviour
    {
        IBuffSystem buffSystem;

        public Transform target;

        public GameObject clickPrefab;

        public GameObject FirePrefab;

        public Transform FirePoint;

        public LayerMask mask;
        //IAstarAI[] ais;
        Camera main;

        public AnimancerComponent animancer;

        private void Awake()
        {          
            main = GetComponent<Camera>();
            //ais = FindObjectsOfType<MonoBehaviour>().OfType<IAstarAI>().ToArray();
        }

        private void Start()
        {
            buffSystem = GameEnter.Buff.CreatBuffSystem();
            entiity entiity = GameObject.Find("GameObject").GetComponent<entiity>();
            //buffSystem.AddBuff(1,entiity.obj[0],entiity.obj[1]);

           
        }

        bool isClick = false;
        bool isAttact = false;

        public void Update()
        {
            //if (Input.GetMouseButtonDown(1))
            //{
            //    //Debug.LogError("点击点击点击点击点击点击");
            //    isClick = true;
            //}

            //if (Input.GetMouseButtonDown(0))
            //{
            //    //Debug.LogError("点击点击点击点击点击点击");
            //    isClick = true;
            //}

            //if (Input.GetKeyDown(KeyCode.X))
            //{

            //    isAttact = true;
            //    Instantiate(FirePrefab,FirePoint.position,FirePoint.rotation);

            //}

            if (Input.GetKeyDown(KeyCode.C))
            {
                GameEnter.Entity.AddEntityGroup("Effect", 3600, 999, 3600, 10);
                GameEnter.Entity.AddEntityGroup("Bullet", 3600, 999, 3600, 10);
                GameEnter.Entity.AddEntityGroup("Player", 3600, 999, 3600, 10);
                GameEnter.Entity.AddEntityGroup("Enemy", 3600, 999, 3600, 10);

                GameEnter.Entity.ShowPlayer(new BulletData(GameEnter.Entity.GenerateSerialId(), 50000, 50000, CampType.Player, 50000, 1)
                {
                    name = "player_Aki",
                    Position = new Vector3(10,0,0)

                });

                GameEnter.Entity.ShowEnemy(new BulletData(GameEnter.Entity.GenerateSerialId(), 50000, 50000, CampType.Player, 50000, 1)
                {
                    name = "Akiiii",
                    Position = new Vector3(0, 0, 0)

                });


            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                //entiity entiity = GameObject.Find("GameObject").GetComponent<entiity>();
                //buffSystem.AddBuff(1, entiity.obj[0], entiity.obj[1]);

                //Debug.LogError(ReferencePool.GetAllReferencePoolInfos()[0].UnusedReferenceCount);


               

                GameEnter.Entity.ShowEnemy(new BulletData(GameEnter.Entity.GenerateSerialId(), 50000, 50000, CampType.Player, 50000, 1)
                {
                    name = "Akiiii",
                    Position = new Vector3(0, 0, 10)

                });
                GameEnter.Entity.ShowEnemy(new BulletData(GameEnter.Entity.GenerateSerialId(), 50000, 50000, CampType.Player, 50000, 1)
                {
                    name = "Akiiii",
                    Position = new Vector3(0, 0, -10)

                });
                GameEnter.Entity.ShowEnemy(new BulletData(GameEnter.Entity.GenerateSerialId(), 50000, 50000, CampType.Player, 50000, 1)
                {
                    name = "Akiiii",
                    Position = new Vector3(-5, 0, 0)

                });
                GameEnter.Entity.ShowEnemy(new BulletData(GameEnter.Entity.GenerateSerialId(), 50000, 50000, CampType.Player, 50000, 1)
                {
                    name = "Akiiii",
                    Position = new Vector3(5, 0, 0)

                });
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

                    //GameObject obj = Instantiate(clickPrefab);
                    //obj.transform.position = hit.point;

                    GameEnter.Entity.ShowEffect(new EffectData(GameEnter.Entity.GenerateSerialId(), 50000) { name= "ClickMove" ,Position=hit.point });


                    //PathFindingTest.workAction.Invoke();


                    //for (int i = 0; i < ais.Length; i++)
                    //{
                    //    ais[i].SearchPath();
                    //}

                    //positionFound = true;
                }
                isClick = false;
            }

            if (isAttact)
            {
                //PathFindingTest.attackAction.Invoke();
                isAttact = false;
            }

           
        }


    }

}
