using System.Collections;
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
    public class ProcedureMDDSkillFactory : ProcedureBase
    {
        List<int> numList = new List<int>();
        List<int> idList = new List<int>();

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);

            Game.NPBehave.GetHelper().PreLoad();

            _ = PreLoadSkillAsset(() =>
              {
                  Game.UI.GetUIForm(UIFormId.LoadingForm).Close();
              });

            Game.Select.isWork = true;

            Log.Info("成功进入训练场景");
        }


        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);


            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
                Game.Entity.ShowPlayer(new PlayerData(Game.Entity.GenerateSerialId(), 10000)
                {
                    Position = new Vector3(0f, 0f, 0f),
                });


                Game.Entity.ShowEnemy(new EnemyData(Game.Entity.GenerateSerialId(), 10001)
                {
                    Position = new Vector3(2f, 0f, 0f),
                });
            }             
        }


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

        private async Task PreLoadSkillAsset(System.Action end)
        {
            IDataTable<DRSkill> dtSkill = Game.DataTable.GetDataTable<DRSkill>();

            DRSkill[] dRSkills = dtSkill.GetAllDataRows();

            for (int i = 0; i < dRSkills.Length; i++)
            {
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

                for (int j = 0; j < numList.Count; j++)
                {
                    for (int k = 0; k < numList[j]; k++)
                    {
                        PreInitEffect(idList[j]);
                    }
                }
            }
          
            await Task.Delay(3000);

            end?.Invoke();
        }


    }


}


