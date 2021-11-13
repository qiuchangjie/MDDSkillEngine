using MDDGameFramework;
using UnityEngine;

namespace MDDGameFramework.Runtime
{
    /// <summary>
    /// 默认游戏框架日志辅助器。
    /// </summary>
    public class DefaultLogHelper : MDDGameFrameworkLog.ILogHelper
    {
        /// <summary>
        /// 记录日志。
        /// </summary>
        /// <param name="level">日志等级。</param>
        /// <param name="message">日志内容。</param>
        public void Log(MDDGameFrameworkLogLevel level, object message)
        {
            switch (level)
            {
                case MDDGameFrameworkLogLevel.Debug:
                    Debug.Log(Utility.Text.Format("<color=#888888>{0}</color>", message));
                    break;

                case MDDGameFrameworkLogLevel.Info:
                    Debug.Log(message.ToString());
                    break;

                case MDDGameFrameworkLogLevel.Warning:
                    Debug.LogWarning(message.ToString());
                    break;

                case MDDGameFrameworkLogLevel.Error:
                    Debug.LogError(message.ToString());
                    break;

                default:
                    throw new MDDGameFrameworkException(message.ToString());
            }
        }
    }
}
