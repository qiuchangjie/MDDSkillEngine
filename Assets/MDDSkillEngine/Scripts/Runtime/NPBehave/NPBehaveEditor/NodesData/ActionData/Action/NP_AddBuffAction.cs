using MDDGameFramework;
using MDDGameFramework.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace MDDSkillEngine
{
    [Title("添加buff", TitleAlignment = TitleAlignments.Centered)]
    public class NP_AddBuffAction : NP_ClassForAction
    {
        [ValueDropdown("GetBuffs")]
        [OnValueChanged("OnBuffsSelect")]
        public string BuffName;

        public override System.Action GetActionToBeDone()
        {
            this.Action = this.AddBuff;
            return this.Action;
        }

        public void AddBuff()
        {
            Entity entity = owner as Entity;
            if (entity == null)
            {
                Log.Error("试图给不是实体的物体添加buff{0}", owner.ToString());
            }

            Game.Buff.AddBuff(entity.Id.ToString(), BuffName, entity, entity);
        }


#if UNITY_EDITOR
        private IEnumerable<string> GetBuffs()
        {
            if (NPBlackBoardEditorInstance.buffs != null)
            {
                return NPBlackBoardEditorInstance.buffs;
            }

            return null;
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


