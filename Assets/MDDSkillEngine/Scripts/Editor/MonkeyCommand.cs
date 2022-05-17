﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonKey;
using MDDGameFramework;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public static class MonkeyCommand
    {
        [Command("AddItem",
            Help = "快速添加物品",
            QuickName = "AI")]
        public static void AddItem(
            [CommandParameter("itemid")]
            int id)
        {
           
        }

        [Command("ChangeState",
            Help = "快速设置角色的状态机黑板值",
            QuickName = "FSM")] 
        public static void ChangeState(
            [CommandParameter("entityID")]
            int id,
            [CommandParameter("state")]
            string state,
            [CommandParameter("state")]
            bool ischange)
        {
           IFsm<Player> fsm = Game.Fsm.GetFsm<Player>("1001");
           fsm.SetData<VarBoolean>("saotangtuiState", true);
        }

        [Command("ChangeState1",
            Help = "快速设置角色的状态机黑板值",
            QuickName = "FSM")]
        public static void ChangeState()
        {
            IFsm<Player> fsm = Game.Fsm.GetFsm<Player>("1001");
            fsm.SetData<VarBoolean>("saotangtuiState", true);
        }

        [Command("testEntity")]
        public static void ShowEntity()
        {
            Game.Entity.ShowCollider(typeof(NormalMoveCollider),new ColliderData(Game.Entity.GenerateSerialId(), 20003, (Entity)Game.Entity.GetEntity(1001).Logic)
            {
                Duration = 7
            });
        }
    }
}

