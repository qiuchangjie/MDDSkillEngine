using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class UIMain : UGuiForm
    {
        [SerializeField]
        private Button SkillListBtn;

        [SerializeField]
        private Button Back;

        [SerializeField]
        private Button Quit;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);

        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            SkillListBtn.onClick.AddListener(OpenSkillList);
            Back.onClick.AddListener(BackToLogin);
            Quit.onClick.AddListener(QuitApplication);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            Log.Info("{0}ui关闭name：{1}", LogConst.UI, Name);
            SkillListBtn.onClick.RemoveListener(OpenSkillList);
            Back.onClick.RemoveListener(BackToLogin);
            Quit.onClick.RemoveListener(QuitApplication);
        }


        private void OpenSkillList()
        {
            Game.UI.OpenUIForm(UIFormId.SkillList);
        }

        private void BackToLogin()
        {
            Game.Procedure.GetFsm().Blackboard.Set<VarInt32>("NextSceneId", 1);
        }

        private void QuitApplication()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }




    }

}

