using UnityEngine;


namespace MDDSkillEngine
{
    public class EnemyData : TargetableObjectData
    {

        public EnemyData(int entityId, int typeId)
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