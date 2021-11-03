using UnityEngine;


namespace MDDSkillEngine
{
    public class PlayerData : TargetableObjectData
    {

        public PlayerData(int entityId, int typeId)
          : base(entityId, typeId, CampType.Neutral)
        {

        }


        public override int MaxHP
        {
            get
            {
                return 666;
            }
        }
    }
}