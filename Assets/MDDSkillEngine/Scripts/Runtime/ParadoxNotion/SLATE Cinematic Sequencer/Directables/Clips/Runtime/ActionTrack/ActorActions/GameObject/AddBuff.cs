using UnityEngine;
using System.Collections;
using MDDSkillEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using MDDGameFramework;
using System;

namespace Slate.ActionClips
{
    [Description("添加buff")]
    [Attachable(typeof(AddBuffTrack))]
    public class AddBuff : ActorActionClip
    {

        [SerializeField]
        [HideInInspector]
        private float _length = 0;

        [ValueDropdown("GetBuffs")]
        [OnValueChanged("OnBuffsSelect")]
        public string buffName;

   
        public override float length
        {
            get { return _length; }
            set { _length = value; }
        }

        protected override void OnEnter()
        {

         
        }

        protected override void OnUpdate(float time, float previousTime)
        {
            base.OnUpdate(time, previousTime);

         
        }

        protected override void OnExit()
        {
           
        }

        protected override void OnReverseEnter()
        {

        }

        protected override void OnReverse()
        {
            
        }

        private void OnSetPosition()
        {
         
        }

#if UNITY_EDITOR
        private IEnumerable<string> GetBuffs()
        {
            if (NPBlackBoardEditorInstance.buffs.Count == 0)
            {
                //通过反射获取所有buff的名字
                List<Type> types = new List<Type>();
                Utility.Assembly.GetTypesByFather(types, typeof(BuffBase));
                List<string> buffsName = new List<string>();
                foreach (var type in types)
                {
                    buffsName.Add(type.Name);
                }
                NPBlackBoardEditorInstance.buffs = buffsName;
            }

            return NPBlackBoardEditorInstance.buffs; ;
        }

        private void OnBuffsSelect()
        {
            if (NPBlackBoardEditorInstance.buffs != null)
            {

            }
        }
#endif

    }
}