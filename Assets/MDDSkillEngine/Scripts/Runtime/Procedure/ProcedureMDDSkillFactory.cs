﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework.Runtime;
using MDDGameFramework;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;
using System;
using UnityEngine.InputSystem;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedureMDDSkillFactory : MDDProcedureBase
    {
        List<int> numList = new List<int>();
        List<int> idList = new List<int>();

        List<int> uGuiForms = new List<int>();

        CoroutineHandler hander;

        protected override void OnInit(ProcedureOwner procedureOwner)
        {
            base.OnInit(procedureOwner);         
        }

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            //hander = PreLoadSkillAsset().Start((b) =>
            //{
            //    ReferencePool.Release(hander);
            //});

            Create().Start();

            Game.NPBehave.GetHelper().PreLoad();

            Game.Select.isWork = true;

            uGuiForms.Clear();

            uGuiForms.Add((int)Game.UI.OpenUIForm(UIFormId.Blackboard));
            uGuiForms.Add((int)Game.UI.OpenUIForm(UIFormId.Ablities));
            uGuiForms.Add((int)Game.UI.OpenUIForm(UIFormId.SkillList));
            uGuiForms.Add((int)Game.UI.OpenUIForm(UIFormId.Drag));
            uGuiForms.Add((int)Game.UI.OpenUIForm(UIFormId.Main));

            Game.Select.InitState();

            Log.Info("成功进入训练场景");
        }


        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);


            //if (Keyboard.current.cKey.wasPressedThisFrame)
            //{
            //    //Game.Entity.ShowPlayer(new PlayerData(1001, 10003)
            //    //{
            //    //    Position = new Vector3(0f, 0f, 0f),
            //    //});

            //    //Game.Entity.ShowPlayer(new PlayerData(1002, 10003)
            //    //{
            //    //    Position = new Vector3(2f, 0f, 0f),
            //    //});

               

            //    //Game.Entity.ShowEntity(typeof(Hero103), "Hero_103", new HeroData(1002, 0,null)
            //    //{
            //    //    Position = new Vector3(2f, 0f, 0f),
            //    //});

            //    //Game.Entity.ShowEnemy(new EnemyData(Game.Entity.GenerateSerialId(), 10001)
            //    //{
            //    //    Position = new Vector3(2f, 0f, 0f),
            //    //});
            //}             
        }

        private IEnumerator Create()
        {
            yield return YieldHelper.WaitForSeconds(2f);

            Game.Entity.ShowEntity(typeof(Player_Normal), "Ai_Player", new PlayerData(1001, 0)
            {
                Position = new Vector3(0f, 0f, 0f),
            });

            Game.Entity.ShowEntity(typeof(Hero103), "Hero_103", new HeroData(1002, 0, null)
            {
                Position = new Vector3(2f, 0f, 0f),
            });
        }
        protected override void OnLeave(ProcedureOwner procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            for (int i = 0; i < uGuiForms.Count; i++)
            {
                if (Game.UI.HasUIForm(uGuiForms[i]))
                {
                    Game.UI.CloseUIForm(uGuiForms[i]);
                }                  
            }

            Game.Select.isWork = false;
        }

        #region 技能需要实体预载预实例
        private void PreInitEffect(int EffectID)
        {
            if (EffectID == 0)
                return;

            Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), EffectID)
            {
                Position = Vector3.zero,
                IsPreLoad = true
            });
        }

        private void PreInitCol(int Colid)
        {
            if (Colid == 0)
                return;

            Game.Entity.ShowCollider(new ColliderData(Game.Entity.GenerateSerialId(), Colid,null)
            {
                Position = Vector3.zero,
                IsPreLoad = true
            });
        }

        private IEnumerator PreLoadSkillAsset()
        {
            IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();

            DRSkill[] dRSkills = dtSkill.GetAllDataRows();

            
            for (int i = 0; i < dRSkills.Length; i++)
            {
                yield return YieldHelper.WaitForEndOfFrame;
                
                for (int j = 0; j < dRSkills[i].EffectAsset.Count; j++)
                {
                    PreInitEffect(dRSkills[i].EffectAsset[j]);
                }

                for (int j = 0; j < dRSkills[i].ColliderEntity.Count; j++)
                {
                    PreInitCol(dRSkills[i].ColliderEntity[j]);
                }


                bool isNum = true;

                for (int j = 0; j < dRSkills[i].EffectAssetMutl.Count; j++)
                {
                    if (isNum)
                    {
                        numList.Add(dRSkills[i].EffectAssetMutl[j]);
                    }
                    else
                    {
                        idList.Add(dRSkills[i].EffectAssetMutl[j]);
                    }
                    isNum = !isNum;
                }

                if (idList.Count != 0)
                {
                    for (int j = 0; j < numList.Count; j++)
                    {
                        for (int k = 0; k < numList[j]; k++)
                        {
                            PreInitEffect(idList[j]);
                        }
                    }
                }
                numList.Clear();
                idList.Clear();
            }

        }

        #endregion

        protected override void Observing(Blackboard.Type type, Variable newValue)
        {
            VarBoolean varBoolean = (VarBoolean)newValue;

            if (varBoolean.Value == false)
                return;
                      
            ChangeState(procedureOwner,GetType());
        }    
    }


}


