using MDDGameFramework;
using MDDGameFramework.Runtime;
using Sirenix.Serialization;

namespace MDDSkillEngine
{
    public class SkillTimelineState<T> : MDDFsmState<T> where T : class
    {
        public SkillTimeline<T> skillTimeline;

        private LoadBinaryCallbacks assetCallbacks;

        private float Duration;

        protected override void OnInit(IFsm<T> fsm)
        {
            base.OnInit(fsm);
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
                Finish(fsm);
            }
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

            skillTimeline.InitSkillClip(skillData);
        }
    }
}
