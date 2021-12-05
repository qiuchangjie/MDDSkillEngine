using System.Collections.Generic;
using System.IO;

namespace MDDSkillEngine
{
    public sealed partial class DataTableProcessor
    {
        private sealed class ListProcessor : GenericDataProcessor<List<int>>
        {
            public override bool IsSystem
            {
                get
                {
                    return true;
                }
            }

            public override bool IsList
            {
                get
                {
                    return true;
                }
            }


            public override string LanguageKeyword
            {
                get
                {
                    return "List<int>";
                }
            }

            public override string[] GetTypeStrings()
            {
                return new string[]
                {
                    "List",
                    "unityengine.List"
                };
            }

           


            public override List<int> Parse(string value)
            {
                string[] splitedValue = value.Split('#');
                List<int> intList = new List<int>();
                for (int i = 0; i < splitedValue.Length; i++)
                {
                    intList.Add(int.Parse(splitedValue[i]));
                }

                return intList;
            }

            public override void WriteToStream(DataTableProcessor dataTableProcessor, BinaryWriter binaryWriter, string value)
            {
                List<int> intList = Parse(value);


                for (int i = 0; i < intList.Count; i++)
                {
                    binaryWriter.Write(intList[i].ToString());
                }

                binaryWriter.Write("#");
            }
        }
    }

}

