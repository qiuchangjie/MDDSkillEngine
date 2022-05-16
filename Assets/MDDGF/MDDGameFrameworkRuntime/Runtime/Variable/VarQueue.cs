using MDDGameFramework;
using System.Collections;

namespace MDDGameFramework.Runtime
{
    /// <summary>
    /// System.Int16 变量类。
    /// </summary>
    public sealed class VarQueue : Variable<Queue>
    {
        /// <summary>
        /// 初始化 System.Int16 变量类的新实例。
        /// </summary>
        public VarQueue()
        {
            Value = new Queue();
        }

        /// <summary>
        /// 从 System.Int16 到 System.Int16 变量类的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        public static implicit operator VarQueue(Queue value)
        {
            VarQueue varValue = ReferencePool.Acquire<VarQueue>();
            varValue.Value = value;
            return varValue;
        }

        /// <summary>
        /// 从 System.Int16 变量类到 System.Int16 的隐式转换。
        /// </summary>
        /// <param name="value">值。</param>
        public static implicit operator Queue(VarQueue value)
        {
            return value.Value;
        }
    }
}
