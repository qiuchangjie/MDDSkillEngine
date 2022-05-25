using MDDGameFramework;
using MDDGameFramework.Runtime;
using Sirenix.Serialization;

namespace MDDSkillEngine
{
    public class SkillTimelineState<T> : MDDFsmState<T> where T : Entity
    {
        public SkillTimeline<T> skillTimeline;

        private LoadBinaryCallbacks assetCallbacks;

        private float Duration;
        IFsm<T> fsm1;

        protected override void OnInit(IFsm<T> fsm)
        {
            base.OnInit(fsm);
            fsm1 = fsm;
            skillTimeline = new SkillTimeline<T>();         
            assetCallbacks = new LoadBinaryCallbacks(LoadCallBack);
            Game.Resource.LoadBinary(AssetUtility.GetSkillTimelineAsset(GetType().Name), assetCallbacks);
        }

        protected override void OnUpdate(IFsm<T> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            Duration += elapseSeconds;
            skillTimeline.Updata(elapseSeconds);

            if (Duration >= skillTimeline.length)
            {
                skillTimeline.SetStateCantStop(false);
                Finish(fsm);
            }
        }

        protected override void OnLeave(IFsm<T> fsm, bool isShutdown)
        {
            base.OnLeave(fsm, isShutdown);
            skillTimeline.currentTime = 0;
            skillTimeline.previousTime = 0;
            skillTimeline.lastTime = 0;
            Duration = 0f;

            skillTimeline.Exit();
        }

        private void LoadCallBack(string entityAssetName, object entityAsset, float duration, object userData)
        {
            Log.Info("{0}加载skilltimeline数据成功 name：{1}", LogConst.SKillTimeline, entityAssetName);

            byte[] data = entityAsset as byte[];

            SkillData skillData = SerializationUtility.DeserializeValue<SkillData>(data, DataFormat.Binary);

            if (skillData == null)
            {
                Log.Error("{0}数据转化失败 name:{1}", LogConst.SKillTimeline, entityAssetName);
            }

            skillTimeline.Init(fsm1, skillData);
        }
    }
}
