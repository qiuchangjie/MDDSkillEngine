
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;
using ProcedureOwner = MDDGameFramework.IFsm<MDDGameFramework.IProcedureManager>;
using MDDGameFramework;

namespace MDDSkillEngine
{
    public class ProcedurePreload : ProcedureBase
    {
        public static readonly string[] DataTableNames = new string[]
        {
            "Aircraft",  
        };

    }
}
