using Animancer;
using MDDGameFramework;
using MDDGameFramework.Runtime;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace MDDSkillEngine
{
    [AkiState]
    public class AkiSkillDataTest : FsmState<Player>
    {

        private ClipState.Transition attack;

        private float attackTime;

        private System.Action endAction;

        private SkillData skillData;

        private void ParseSkillData(SkillData skillData, IFsm<Player> fsm)
        {
            bool effect=false;

            foreach (var data in skillData.skillData)
            {
                switch (data.DataType)
                {
                    case SkillDataType.Effect:
                        {
                            if (data.StartTime <= fsm.CurrentStateTime && !effect)
                            {
                                Log.Info("执行effectdata");
                                Game.Entity.ShowEffect(new EffectData(Game.Entity.GenerateSerialId(), 70006)
                                {
                                    Position = fsm.Owner.CachedTransform.position,
                                    Rotation = fsm.Owner.CachedTransform.rotation,
                                    KeepTime = 5f
                                });
                                effect = true;
                            }
                        }
                        break;

                }
            }
        }

        protected override void OnInit(IFsm<Player> fsm)
        {
            base.OnInit(fsm);

            string savePath = Application.dataPath + "/MDDSkillEngine/SkillData/";
            byte[] bytes1 = File.ReadAllBytes(savePath + "name.bytes");
            skillData = SerializationUtility.DeserializeValue<SkillData>(bytes1, DataFormat.Binary);


            fsm.SetData<VarBoolean>("skilldatatest", false);

            Log.Error("数据加载并反序列化成功");
        }

        protected override void OnEnter(IFsm<Player> fsm)
        {
            base.OnEnter(fsm);
            Log.Info("进入test状态");   
        }

        protected override void OnDestroy(IFsm<Player> fsm)
        {
            base.OnDestroy(fsm);
        
        }

        protected override void OnLeave(IFsm<Player> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
         
        }

        protected override void OnUpdate(IFsm<Player> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            ParseSkillData(skillData, fsm);
        }
    }
}

