using System;


namespace MDDGameFramework
{
    public abstract class MDDGameFromeworkEventArgs : EventArgs,IReference
    {
        /// <summary>
        /// 初始化游戏框架中包含事件数据的类的新实例。
        /// </summary>
        public MDDGameFromeworkEventArgs()
        {
        }

        /// <summary>
        /// 清理引用。
        /// </summary>
        public abstract void Clear();
          
    }
}


