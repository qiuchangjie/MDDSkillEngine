using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework.Runtime;
using MDDGameFramework;

public sealed class TestEventArgs : GameEventArgs
{
    /// <summary>
    /// 事件编号。
    /// </summary>
    public static readonly int EventId = typeof(TestEventArgs).GetHashCode();

    /// <summary>
    /// 获取事件编号
    /// </summary>
    public override int Id
    {
        get
        {
            return EventId;
        }
    }
    /// <summary>
    /// 输出字段
    /// </summary>
    public string logString
    {
        get;
        private set;
    }

    /// <summary>
    /// 创建事件
    /// </summary>
    /// <param name="e">内部事件。</param>
    /// <returns>创建的事件。</returns>
    public static TestEventArgs Create(string logString)
    {
        // 使用引用池技术，避免频繁内存分配
        TestEventArgs e = ReferencePool.Acquire<TestEventArgs>();
        e.logString = logString;
        return e;
    }

    public override void Clear()
    {
        logString = null;
    }
}
