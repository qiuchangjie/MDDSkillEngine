using System;
using System.Collections;
using System.Collections.Generic;

namespace MDDGameFramework
{

    public interface IBuffFactory
    {
         BuffBase AcquireBuff(string bufName, object Target, object From,object userData=null);
    }
}
