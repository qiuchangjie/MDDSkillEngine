using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MDDSkillEngine
{
    public class Hero103Data : HeroData
    {
  

        public Hero103Data(int entityId, int typeId, Entity Owner) : base(entityId, typeId, Owner)
        {
     
        }
    
        public override float MaxHP
        {
            get
            {
                return 999;
            }
        }
    }
}
