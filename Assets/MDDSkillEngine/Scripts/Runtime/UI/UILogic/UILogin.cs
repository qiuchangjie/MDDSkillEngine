﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class UILogin : UGuiForm
    {       
        [SerializeField]
        private Button skillLaunch;

        [SerializeField]
        private Button kaer;

        [SerializeField]
        private Button guiqi;

        [SerializeField]
        private Button timescaledemo;

        [SerializeField]
        private Button about;


        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            about.onClick.AddListener(AboutAction);
            skillLaunch.onClick.AddListener(SkillLaunchAction);
            timescaledemo.onClick.AddListener(TimeScaleAction);
            kaer.onClick.AddListener(KaerAction);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            Log.Info("{0}ui关闭name：{1}",LogConst.UI,Name);
        }

        private void SkillLaunchAction()
        {
            Game.Procedure.GetFsm().Blackboard.Set<VarInt32>("NextSceneId", 2);
            Close(false);
        }

        private void KaerAction()
        {
            Game.Procedure.GetFsm().Blackboard.Set<VarInt32>("NextSceneId", 3);
            Close(false);
        }

        private void TimeScaleAction()
        {
            Game.Procedure.GetFsm().Blackboard.Set<VarInt32>("NextSceneId", 4);
            Close(false);
        }

        private void AboutAction()
        {
            Application.OpenURL("http://www.maodaodao.top/");
        }
    }

}

