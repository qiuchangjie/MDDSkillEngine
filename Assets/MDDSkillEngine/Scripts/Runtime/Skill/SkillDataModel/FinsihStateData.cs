using MDDGameFramework.Runtime;
using Slate;
using Slate.ActionClips;
using System;
using UnityEngine;


namespace MDDSkillEngine
{
    [Serializable]
    public class FinsihStateData : SkillDataBase
    {
# if UNITY_EDITOR
        public override void OnInit(ActionClip data)
        {
            base.OnInit(data);
            DataType = SkillDataType.FinishState;
        }
#endif
    }
}

