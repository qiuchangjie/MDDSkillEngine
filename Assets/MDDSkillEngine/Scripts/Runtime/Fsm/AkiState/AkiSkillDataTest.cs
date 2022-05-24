//using Animancer;
//using MDDGameFramework;
//using MDDGameFramework.Runtime;
//using Sirenix.Serialization;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;

//namespace MDDSkillEngine
//{
//    [AkiState]
//    public class AkiSkillDataTest : MDDFsmState<Entity>
//    {

//        private ClipState.Transition attack;

//        private float attackTime;

//        private System.Action endAction;

//        private SkillData skillData;

//        bool effect = false;
//        bool anim = false;

//        private void ParseSkillData(SkillData skillData, IFsm<Entity> fsm)
//        {
//           // bool effect=false;

//            foreach (var data in skillData.skillData)
//            {
//                switch (data.DataType)
//                {
//                    case SkillDataType.Effect:
//                        {
//                            EffectSkillData eff = (EffectSkillData)data;    

//                            if (data.StartTime <= fsm.CurrentStateTime && !effect)
//                            {
//                                Log.Info("执行effectdata");

//                                int id = Game.Entity.GenerateSerialId();

//                                Game.Entity.ShowEffect(new EffectData(id, 70006)
//                                {
//                                    Position = fsm.Owner.CachedTransform.position,
//                                    Rotation = fsm.Owner.CachedTransform.rotation,
//                                    KeepTime = 2f
//                                });
//                                MDDGameFramework.Runtime.Entity e = Game.Entity.GetEntity(id);
//                                if (e != null)
//                                    Log.Error(e.name);
//                                Game.Entity.AttachEntity(id, fsm.Owner.Id);

//                                e.Logic.CachedTransform.localPosition = eff.localeftPostion;
//                                e.Logic.CachedTransform.localRotation = eff.localRotation;
//                                e.Logic.CachedTransform.localScale = eff.localScale;

//                                effect = true;
//                            }
//                        }
//                        break;
//                    case SkillDataType.Animation:
//                        {
//                            if (!anim)
//                            {
//                                fsm.Owner.CachedAnimancer.Play(attack);
//                                Log.Error("播放动画数据");
//                                anim = true;
//                            }                            
//                        }
//                        break;

//                }
//            }
//        }

//        protected override void OnInit(IFsm<Entity> fsm)
//        {
//            base.OnInit(fsm);

//            string savePath = Application.dataPath + "/MDDSkillEngine/SkillRes/";
//            byte[] bytes1 = File.ReadAllBytes(savePath + "name.bytes");

//            skillData = SerializationUtility.DeserializeValue<SkillData>(bytes1, DataFormat.Binary);


//            fsm.SetData<VarBoolean>("skilldatatest", false);
//            attack = fsm.Owner.CachedAnimContainer.GetAnimation("Attack1");
//            Log.Error("数据加载并反序列化成功");
//        }

//        protected override void OnEnter(IFsm<Entity> fsm)
//        {
//            base.OnEnter(fsm);
//            effect = false;
//            anim = false;
//            Log.Info("进入test状态");   
//        }

//        protected override void OnDestroy(IFsm<Entity> fsm)
//        {
//            base.OnDestroy(fsm);
        
//        }

//        protected override void OnLeave(IFsm<Entity> fsm, bool isShutdown)
//        {
//            base.OnLeave(fsm, isShutdown);
//            fsm.SetData<VarBoolean>("skilldatatest", false);
//        }

//        protected override void OnUpdate(IFsm<Entity> fsm, float elapseSeconds, float realElapseSeconds)
//        {
//            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
//            ParseSkillData(skillData, fsm);
//            if (duration >= skillData.Length)
//            {
//                Log.Error(skillData.Length);
//                Finish(fsm);
//            }

//        }
//    }
//}

