
using MDDGameFramework;
using UnityEditor;
using UnityEngine;

namespace MDDSkillEngine
{
    public sealed class DataTableGeneratorMenu
    {
        [MenuItem("MDDSkillEngine/Generate DataTables")]
        private static void GenerateDataTables()
        {
            foreach (string dataTableName in ProcedurePreload.DataTableNames)
            {
                DataTableProcessor dataTableProcessor = DataTableGenerator.CreateDataTableProcessor(dataTableName);
                if (!DataTableGenerator.CheckRawData(dataTableProcessor, dataTableName))
                {
                    Debug.LogError(Utility.Text.Format("Check raw data failure. DataTableName='{0}'", dataTableName));
                    break;
                }

                DataTableGenerator.GenerateDataFile(dataTableProcessor, dataTableName);
                DataTableGenerator.GenerateCodeFile(dataTableProcessor, dataTableName);
            }

            AssetDatabase.Refresh();
        }
    }
}
