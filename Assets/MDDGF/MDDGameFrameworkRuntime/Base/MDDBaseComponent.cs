using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDDGameFramework;

namespace MDDGameFramework.Runtime
{
    public sealed class MDDBaseComponent : MDDGameFrameworkComponent
    {

        [SerializeField]
        private string m_TextHelperTypeName = "MDDGameFramework.Runtime.DefaultTextHelper";

        [SerializeField]
        private string m_VersionHelperTypeName = "UnityGameFramework.Runtime.DefaultVersionHelper";

        [SerializeField]
        private string m_LogHelperTypeName = "MDDGameFramework.Runtime.DefaultLogHelper";

        [SerializeField]
        private string m_CompressionHelperTypeName = "UnityGameFramework.Runtime.DefaultCompressionHelper";

        [SerializeField]
        private string m_JsonHelperTypeName = "UnityGameFramework.Runtime.DefaultJsonHelper";

        protected override void Awake()
        {

            base.Awake();

            InitTextHelper();
            InitLogHelper();
        }


        private void Update()
        {
            MDDGameFrameworkEntry.Update(Time.deltaTime,Time.unscaledDeltaTime);
        }

        private void InitTextHelper()
        {
            if (string.IsNullOrEmpty(m_TextHelperTypeName))
            {
                return;
            }

            Type textHelperType = Utility.Assembly.GetType(m_TextHelperTypeName);
            if (textHelperType == null)
            {
                Log.Error("Can not find text helper type '{0}'.", m_TextHelperTypeName);
                return;
            }

            Utility.Text.ITextHelper textHelper = (Utility.Text.ITextHelper)Activator.CreateInstance(textHelperType);
            if (textHelper == null)
            {
                Log.Error("Can not create text helper instance '{0}'.", m_TextHelperTypeName);
                return;
            }

            Utility.Text.SetTextHelper(textHelper);
        }


        private void InitLogHelper()
        {
            if (string.IsNullOrEmpty(m_LogHelperTypeName))
            {
                return;
            }

            Type logHelperType = Utility.Assembly.GetType(m_LogHelperTypeName);
            if (logHelperType == null)
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("Can not find log helper type '{0}'.", m_LogHelperTypeName));
            }

            MDDGameFrameworkLog.ILogHelper logHelper = (MDDGameFrameworkLog.ILogHelper)Activator.CreateInstance(logHelperType);
            if (logHelper == null)
            {
                throw new MDDGameFrameworkException(Utility.Text.Format("Can not create log helper instance '{0}'.", m_LogHelperTypeName));
            }

            MDDGameFrameworkLog.SetLogHelper(logHelper);
        }
    }
}

