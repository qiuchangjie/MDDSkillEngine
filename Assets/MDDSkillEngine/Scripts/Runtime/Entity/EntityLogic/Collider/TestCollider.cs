using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using MDDGameFramework;

namespace MDDSkillEngine
{
    public class TestCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Entity entity = other.gameObject.GetComponent<Entity>();

            if (entity == null)
            {
                return;
            }
 
          Game.Buff.AddBuff(entity.Id.ToString(), "TimeScaleBuff", entity, null);
        }

        private void OnTriggerExit(Collider other)
        {
            Entity entity = other.gameObject.GetComponent<Entity>();

            if (entity == null)
            {
                return;
            }

            IBuffSystem buffSystem = Game.Buff.GetBuffSystem(entity.Id.ToString());

            if (buffSystem != null)
            {
                buffSystem.RemoveBuff(Utility.Assembly.GetType(Utility.Text.Format("MDDSkillEngine.{0}", "TimeScaleBuff")));
            }
        }
    }
}


