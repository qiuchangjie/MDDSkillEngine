
using MDDGameFramework;

namespace MDDSkillEngine
{
    public class NormalData : BuffDatabase
    {
        public NormalData() { }

        private void Init(DRBuff dRBuff)
        {
            Init(dRBuff.Id, dRBuff.Level, dRBuff.Duration);
        }

        public static NormalData Create(DRBuff dRBuff)
        {
            NormalData normalData = ReferencePool.Acquire<NormalData>();
            normalData.Init(dRBuff);

            return normalData;
        }
    }

}

