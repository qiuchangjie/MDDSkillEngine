using MDDGameFramework;
using MDDGameFramework.Runtime;
namespace MDDSkillEngine
{
    public class SkillTimelineState<T> : FsmState<T> where T : class
    {
        public SkillTimeline<T> skillTimeline;

        private LoadBinaryCallbacks assetCallbacks;

        protected override void OnInit(IFsm<T> fsm)
        {
            base.OnInit(fsm);
            skillTimeline = new SkillTimeline<T>();

            assetCallbacks = new LoadBinaryCallbacks(LoadCallBack);
            Game.Resource.LoadBinary(AssetUtility.GetSkillAsset(""), assetCallbacks);
        }

        protected override void OnUpdate(IFsm<T> fsm, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
            skillTimeline.Updata(elapseSeconds);
        }

        private void LoadCallBack(string entityAssetName, object entityAsset, float duration, object userData)
        {
            Log.Info("{0}加载skilltimeline数据成功 name：{1}", LogConst.SKillTimeline, entityAssetName);

            SkillData skillData = entityAsset as SkillData;

            if (skillData == null)
            {
                Log.Error("{0}数据转化失败 name:{1}", LogConst.SKillTimeline, entityAssetName);
            }

            skillTimeline.InitSkillClip(skillData);
        }
    }
}
