using MDDGameFramework;
using MDDGameFramework.Runtime;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDSkillEngine
{
    public abstract class SkillTimelineState<T> : MDDFsmState<T> where T : Entity
    {
        /// <summary>
        /// 一个简易的timeline池子
        /// </summary>
        private Queue<SkillTimeline<T>> skillTimelineQuene;

        /// <summary>
        /// 在运行中的协程字典
        /// </summary>
        private Dictionary<SkillTimeline<T>, CoroutineHandler> coroutineHandlerDic;


        /// <summary>
        /// skilltimeline数据资源加载回调
        /// </summary>
        private LoadBinaryCallbacks assetCallbacks;

        /// <summary>
        ///  skilltimeline数据
        /// </summary>
        private SkillData skillData;

        /// <summary>
        /// 缓存一下委托防止频繁GC
        /// </summary>
        private System.Action<bool> SkillTimelineEndCallBack;

       
        private float Duration;

        protected override void OnInit(IFsm<T> fsm)
        {
            base.OnInit(fsm);
            skillTimelineQuene = new Queue<SkillTimeline<T>>();
            coroutineHandlerDic = new Dictionary<SkillTimeline<T>, CoroutineHandler>();
            assetCallbacks = new LoadBinaryCallbacks(LoadCallBack);
            Game.Resource.LoadBinary(AssetUtility.GetSkillTimelineAsset(GetType().Name), assetCallbacks);
        }

        protected override void OnEnter(IFsm<T> fsm)
        {
            base.OnEnter(fsm);

            //根据timeline池开启协程
            if (skillTimelineQuene.Count == 0)
            {
                SkillTimeline<T> skillTimeline = new SkillTimeline<T>();
                skillTimeline.Init(fsm, skillData);
                //开启协程
                coroutineHandlerDic.Add(skillTimeline, UpdateSkillTimeline(skillTimeline).Start());
                Log.Info("{0}储备不足创建新的skilltimeline", LogConst.FSM);
            }
            else
            {
                SkillTimeline<T> skillTimeline = skillTimelineQuene.Dequeue();
                //开启协程
                coroutineHandlerDic.Add(skillTimeline, UpdateSkillTimeline(skillTimeline).Start());              
                Log.Info("{0}使用储备的skilltimeline", LogConst.FSM);
            }
        }

        protected override void OnUpdate(IFsm<T> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Duration += elapseSeconds;

            if (FinishTime != 0f)
            {
                if (Duration >= FinishTime)
                {
                    Finish(fsm);
                }
            }           
        }

        protected override void OnLeave(IFsm<T> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);

            Duration = 0f;
        }

        /// <summary>
        /// skilltimeline驱动函数
        /// </summary>
        /// <param name="skillTimeline"></param>
        /// <returns></returns>
        public IEnumerator UpdateSkillTimeline(SkillTimeline<T> skillTimeline)
        {
            float DurationTime = 0;

            Log.Error("{0}回收skilltimeline{1}", LogConst.SKillTimeline, DurationTime);

            while (DurationTime <= skillTimeline.length)
            {               
                skillTimeline.Updata(Time.deltaTime);
                DurationTime += Time.deltaTime;
                yield return YieldHelper.WaitForEndOfFrame;
            }

            skillTimeline.Exit();

            ReferencePool.Release(coroutineHandlerDic[skillTimeline]);
            coroutineHandlerDic.Remove(skillTimeline);           
            skillTimelineQuene.Enqueue(skillTimeline);
            Log.Error("{0}回收skilltimeline{1}.count{2}", LogConst.SKillTimeline, DurationTime, skillTimelineQuene.Count);

            //if (Duration != 0f)
            //    Finish(Fsm);
        }



        private void LoadCallBack(string entityAssetName, object entityAsset, float duration, object userData)
        {
            Log.Info("{0}加载skilltimeline数据成功 name：{1}", LogConst.SKillTimeline, entityAssetName);

            byte[] data = entityAsset as byte[];

            skillData = SerializationUtility.DeserializeValue<SkillData>(data, DataFormat.Binary);

            if (skillData == null)
            {
                Log.Error("{0}数据转化失败 name:{1}", LogConst.SKillTimeline, entityAssetName);
            }
            FinishTime = skillData.FinishStateTime;
            SkillTimeline<T> skillTimeline = new SkillTimeline<T>();
            skillTimeline.Init(Fsm, skillData);
            skillTimelineQuene.Enqueue(skillTimeline);
        }
    }
}
