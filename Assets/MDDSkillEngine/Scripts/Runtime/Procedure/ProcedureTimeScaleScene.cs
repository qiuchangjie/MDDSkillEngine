
using MDDGameFramework.Runtime;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework;
using UnityEngine.InputSystem;

namespace MDDSkillEngine
{
    [Procedure]
    public class ProcedureTimeScaleScene : MDDProcedureBase
    {
        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Game.UI.GetUIForm(UIFormId.LoadingForm).Close(false);
            Game.Select.isWork = true;
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            if (Keyboard.current.cKey.wasPressedThisFrame)
            {
               IEntityGroup entityGroup = Game.Entity.GetEntityGroup("Player");
               if (entityGroup != null)
                {
                    for (int i = 0; i < entityGroup.GetAllEntities().Length; i++)
                    {
                        int id = entityGroup.GetAllEntities()[i].Id;
                        Entity entity = Game.Entity.GetGameEntity(id);
                        if (entity != null)
                        {
                            Log.Error("****************");
                            Game.Buff.AddBuff(entity.Id.ToString(), "PlaybleSpeedTestBuff", entity, null);
                        }
                    }
                    
                }
            }
        }
    }
}
