
using MDDGameFramework;
using UnityEngine;

namespace MDDSkillEngine
{
    public class NormalHitData : BuffDatabase
    {
        public Vector3 hitDir = Vector3.zero;

        public NormalHitData() { }

        private void Init(DRBuff dRBuff, object userdata = null)
        {
            Init(dRBuff.Id, dRBuff.Level, dRBuff.Duration);
        }

        public static NormalHitData Create(DRBuff dRBuff,object userdata=null)
        {
            NormalHitData normalData = ReferencePool.Acquire<NormalHitData>();
            normalData.Init(dRBuff,userdata);

            return normalData;
        }
    }

}

