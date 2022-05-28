using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MDDGameFramework.Runtime;

namespace MDDSkillEngine
{
    public class SkillSlot : MonoBehaviour,IBeginDragHandler, IDragHandler,IEndDragHandler
    {
        [SerializeField]
        private Text SkillName;
        private int id = 0;

        public void Init(DRSkill dRSkill)
        {
            SkillName.text = dRSkill.Name;
            id = dRSkill.Id;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (id != 0)
            {
                Game.Event.Fire(this, BeginDragSkillSlotEventArgs.Create(id, SkillName.text));
            }           
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (id != 0)
            {
                Game.Event.Fire(this, DragSkillSlotEventArgs.Create());
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (id != 0)
            {
                if (eventData.pointerCurrentRaycast.gameObject == null)
                {
                    Game.Event.Fire(this, EndDragSkillSlotEventArgs.Create(false));
                    return;
                }

                AblitiesSlot target = eventData.pointerCurrentRaycast.gameObject.GetComponent<AblitiesSlot>();

                if (target == null)
                {
                    Game.Event.Fire(this, EndDragSkillSlotEventArgs.Create(false));
                }
                else
                {
                    Game.Event.Fire(this, EndDragSkillSlotEventArgs.Create(true,target, id));
                }



                Game.Event.Fire(this, EndDragSkillSlotEventArgs.Create(false));
            }
        }
    }
}


