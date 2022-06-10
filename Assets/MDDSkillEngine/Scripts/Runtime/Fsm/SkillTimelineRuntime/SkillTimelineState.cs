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
        private Queue<SkillTimeline<T>> skillTimelineQuene;
        private LoadBinaryCallbacks assetCallbacks;

        private SkillData skillData;

        private System.Action<bool> SkillTimelineEndCallBack;
        private CoroutineHandler coroutineHandler;
        private float Duration;

        protected override void OnInit(IFsm<T> fsm)
        {
            base.OnInit(fsm);
            skillTimelineQuene = new Queue<SkillTimeline<T>>();
            assetCallbacks = new LoadBinaryCallbacks(LoadCallBack);
            SkillTimelineEndCallBack += EndCallBack;
            Game.Resource.LoadBinary(AssetUtility.GetSkillTimelineAsset(GetType().Name), assetCallbacks);
        }

        protected override void OnEnter(IFsm<T> fsm)
        {
            base.OnEnter(fsm);

            if (skillTimelineQuene.Count == 0)
            {
                SkillTimeline<T> skillTimeline = new SkillTimeline<T>();
                skillTimeline.Init(fsm, skillData);
                coroutineHandler = UpdateSkillTimeline(skillTimeline).Start(SkillTimelineEndCallBack);
                Log.Info("{0}储备不足创建新的skilltimeline", LogConst.FSM);
            }
            else
            {
                SkillTimeline<T> skillTimeline = skillTimelineQuene.Dequeue();
                coroutineHandler = UpdateSkillTimeline(skillTimeline).Start(SkillTimelineEndCallBack);
            }


        }

        protected override void OnUpdate(IFsm<T> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Duration += elapseSeconds;
        }

        protected override void OnLeave(IFsm<T> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);

            Duration = 0f;

            //skillTimeline.Exit();
        }

        public IEnumerator UpdateSkillTimeline(SkillTimeline<T> skillTimeline)
        {
            float DurationTime = 0;

            while (DurationTime <= skillTimeline.length)
            {
                DurationTime += Time.deltaTime;
                skillTimeline.Updata(Time.deltaTime);
                yield return YieldHelper.WaitForEndOfFrame;
            }

            skillTimeline.Exit();
            skillTimelineQuene.Enqueue(skillTimeline);
            Log.Info("{0}回收skilltimeline", LogConst.FSM);
            Finish(Fsm);
        }

        private void EndCallBack(bool b)
        {
            if (coroutineHandler != null)
            {
                ReferencePool.Release(coroutineHandler);
            }
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

            SkillTimeline<T> skillTimeline = new SkillTimeline<T>();
            skillTimeline.Init(Fsm, skillData);
            skillTimelineQuene.Enqueue(skillTimeline);
        }
    }
}
