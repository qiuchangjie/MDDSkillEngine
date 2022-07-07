using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework.Runtime
{
    public class Skill : NP_Tree
    {
        public Skill()
        {
            
        }

        public static Skill Create()
        {
            Skill skill = ReferencePool.Acquire<Skill>();

            return skill;
        }

        public override void Init(Root root)    
        {
            base.Init(root);
        }
    }
}


