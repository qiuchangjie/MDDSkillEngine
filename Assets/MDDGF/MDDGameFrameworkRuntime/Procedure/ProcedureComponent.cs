
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MDDGameFramework.Runtime
{
    /// <summary>
    /// 流程组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Framework/Procedure")]
    public sealed class ProcedureComponent : MDDGameFrameworkComponent
    {
        private IProcedureManager m_ProcedureManager = null;
        private ProcedureBase m_EntranceProcedure = null;

        [SerializeField]
        private string[] m_AvailableProcedureTypeNames = null;

        [SerializeField]
        private string m_EntranceProcedureTypeName = null;

        /// <summary>
        /// 获取当前流程。
        /// </summary>
        public ProcedureBase CurrentProcedure
        {
            get
            {
                return m_ProcedureManager.CurrentProcedure;
            }
        }

        /// <summary>
        /// 获取当前流程持续时间。
        /// </summary>
        public float CurrentProcedureTime
        {
            get
            {
                return m_ProcedureManager.CurrentProcedureTime;
            }
        }

        /// <summary>
        /// 游戏框架组件初始化。
        /// </summary>
        protected override void Awake()
        {
            base.Awake();

            m_ProcedureManager = MDDGameFrameworkEntry.GetModule<IProcedureManager>();
            if (m_ProcedureManager == null)
            {
                Log.Fatal("Procedure manager is invalid.");
                return;
            }
        }

        private IEnumerator Start()
        {
            List<Type> proceduresTypes = new List<Type>();

            Utility.Assembly.GetTypesByAttribute<ProcedureAttribute>(proceduresTypes,typeof(FsmState));

            ProcedureBase[] procedures = new ProcedureBase[proceduresTypes.Count];

            for (int i = 0; i < proceduresTypes.Count; i++)
            {
                procedures[i] = (ProcedureBase)Activator.CreateInstance(proceduresTypes[i]);

                if (procedures[i] == null)
                {
                    Log.Error("Can not create procedure instance '{0}'.", m_AvailableProcedureTypeNames[i]);
                    yield break;
                }

                if (procedures[i].GetType().FullName == m_EntranceProcedureTypeName)
                {
                    m_EntranceProcedure = procedures[i];
                }

                if (m_EntranceProcedure == null)
                {
                    Log.Error("Entrance procedure is invalid.");
                    yield break;
                }              
            }

            m_ProcedureManager.Initialize(MDDGameFrameworkEntry.GetModule<IFsmManager>(), procedures);

            yield return new WaitForEndOfFrame();

            m_ProcedureManager.StartProcedure(m_EntranceProcedure.GetType());


            //for (int i = 0; i < m_AvailableProcedureTypeNames.Length; i++)
            //{
            //    Type procedureType = Utility.Assembly.GetType(m_AvailableProcedureTypeNames[i]);
            //    if (procedureType == null)
            //    {
            //        Log.Error("Can not find procedure type '{0}'.", m_AvailableProcedureTypeNames[i]);
            //        yield break;
            //    }

            //    procedures[i] = (ProcedureBase)Activator.CreateInstance(procedureType);
            //    if (procedures[i] == null)
            //    {
            //        Log.Error("Can not create procedure instance '{0}'.", m_AvailableProcedureTypeNames[i]);
            //        yield break;
            //    }

            //    if (m_EntranceProcedureTypeName == m_AvailableProcedureTypeNames[i])
            //    {
            //        m_EntranceProcedure = procedures[i];
            //    }
            //}

            //if (m_EntranceProcedure == null)
            //{
            //    Log.Error("Entrance procedure is invalid.");
            //    yield break;
            //}

            //m_ProcedureManager.Initialize(MDDGameFrameworkEntry.GetModule<IFsmManager>(), procedures);

            //yield return new WaitForEndOfFrame();

            //m_ProcedureManager.StartProcedure(m_EntranceProcedure.GetType());
        }

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <typeparam name="T">要检查的流程类型。</typeparam>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure<T>() where T : ProcedureBase
        {
            return m_ProcedureManager.HasProcedure<T>();
        }

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <param name="procedureType">要检查的流程类型。</param>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure(Type procedureType)
        {
            return m_ProcedureManager.HasProcedure(procedureType);
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <typeparam name="T">要获取的流程类型。</typeparam>
        /// <returns>要获取的流程。</returns>
        public ProcedureBase GetProcedure<T>() where T : ProcedureBase
        {
            return m_ProcedureManager.GetProcedure<T>();
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <param name="procedureType">要获取的流程类型。</param>
        /// <returns>要获取的流程。</returns>
        public ProcedureBase GetProcedure(Type procedureType)
        {
            return m_ProcedureManager.GetProcedure(procedureType);
        }
    }
}
