using System;
using System.Collections;
using System.Collections.Generic;

namespace MDDGameFramework
{
    /// <summary>
    /// 暴露在外的辅助buff生成接口
    /// </summary>
    public interface IBuffFactory
    {
         BuffBase AcquireBuff(string bufName, object Target, object From,object userData=null);
    }
}
