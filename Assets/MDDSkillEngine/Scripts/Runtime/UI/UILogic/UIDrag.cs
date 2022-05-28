using MDDGameFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MDDGameFramework;
using UnityEngine.InputSystem;

namespace MDDSkillEngine
{
    public class UIDrag : UGuiForm
    {
        [SerializeField]
        private DragSkillSlot skillSlot;

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            skillSlot.gameObject.SetActive(false);
        }

        protected override void OnOpen(object userData)
        {
            base.OnOpen(userData);
            Game.Event.Subscribe(BeginDragSkillSlotEventArgs.EventId, OnBeginDrag);
            Game.Event.Subscribe(DragSkillSlotEventArgs.EventId, OnDrag);
            Game.Event.Subscribe(EndDragSkillSlotEventArgs.EventId, OnEndDrag);
        }

        protected override void OnClose(bool isShutdown, object userData)
        {
            base.OnClose(isShutdown, userData);
            Log.Info("{0}ui关闭name：{1}", LogConst.UI, Name);
            Game.Event.Unsubscribe(BeginDragSkillSlotEventArgs.EventId, OnBeginDrag);
            Game.Event.Unsubscribe(DragSkillSlotEventArgs.EventId, OnDrag);
            Game.Event.Unsubscribe(EndDragSkillSlotEventArgs.EventId, OnEndDrag);
        }


        private void OnBeginDrag(object sender, GameEventArgs e)
        {
            BeginDragSkillSlotEventArgs args = (BeginDragSkillSlotEventArgs)e;

            skillSlot.Init(args.SkillID, args.SkillName);

            skillSlot.gameObject.SetActive(true);

            Vector2 outVec;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, Mouse.current.position.ReadValue(), Game.Scene.UICamera, out outVec);
            ((RectTransform)skillSlot.transform).anchoredPosition = outVec;
        }

        private void OnDrag(object sender, GameEventArgs e)
        {
            Vector2 outVec;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, Mouse.current.position.ReadValue(), Game.Scene.UICamera, out outVec);
            ((RectTransform)skillSlot.transform).anchoredPosition = outVec;
        }

        private void OnEndDrag(object sender, GameEventArgs e)
        {
            skillSlot.gameObject.SetActive(false);

            EndDragSkillSlotEventArgs args = (EndDragSkillSlotEventArgs)e;

            if (args.isReycast)
            {
                if (args.ablitiesSlot.SkillID != 0)
                {
                    Game.Skill.GetSkillSystem(1001).RemoveSkill(args.ablitiesSlot.SkillID);
                }

                Game.Skill.GetSkillSystem(1001).AddSkill(args.SkillID, args.ablitiesSlot.index);
            }
        }
    }

}

