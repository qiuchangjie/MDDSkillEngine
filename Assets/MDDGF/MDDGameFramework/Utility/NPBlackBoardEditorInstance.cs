#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;

namespace MDDGameFramework
{
    public static class NPBlackBoardEditorInstance 
    {
        public static Dictionary<string, Variable> BBValues = new Dictionary<string, Variable>();
    }

}

#endif