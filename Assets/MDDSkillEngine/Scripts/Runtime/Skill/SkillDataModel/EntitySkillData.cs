using MDDGameFramework.Runtime;
using Slate;
using Slate.ActionClips;
using System;
using UnityEngine;


namespace MDDSkillEngine
{
    [Serializable]
    public class EntitySkillData : SkillDataBase
    {
        public Vector3 localeftPostion;
        public Quaternion localRotation;
        public Vector3 localScale;


        public string EntityLogic;
        public string EntityName;

# if UNITY_EDITOR
        public override void OnInit(ActionClip data)
        {
            base.OnInit(data);
            DataType = SkillDataType.Entity;

            EntityInstance EntityData = data as EntityInstance;

            EntityLogic = EntityData.EntityLogic;
            EntityName = EntityData.EntityName;
            localeftPostion = EntityData.localeftPostion;
            localRotation = EntityData.localRotation;
            localScale = EntityData.localScale;
        }
#endif
    }

}

