using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class DragSkillSlot : MonoBehaviour,IDragHandler,IEndDragHandler
    {
        [SerializeField]
        private Text SkillName;
        private int id = 0;

        public void Init(DRSkill dRSkill)
        {
            SkillName.text = dRSkill.Name;
            id = dRSkill.Id;
        }

        public void Init(int id,string skillname)
        {
            SkillName.text = skillname;
            this.id = id;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 outVec;
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)transform, Mouse.current.position.ReadValue(), Game.Scene.UICamera, out outVec);
            ((RectTransform)transform).anchoredPosition = outVec;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Log.Error("{0}OnEndDrag{1}", eventData.selectedObject.name);
        }
    }
}


