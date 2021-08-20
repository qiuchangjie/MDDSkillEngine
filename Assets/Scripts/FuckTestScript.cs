using OdinSerializer;
using System.IO;

public static class Example
{
	public static void Save(MyData data, string filePath)
	{
		byte[] bytes = SerializationUtility.SerializeValue(data, DataFormat.Binary);
		File.WriteAllBytes(filePath, bytes);
	}

	public static MyData Load(string filePath)
	{
		byte[] bytes = File.ReadAllBytes(filePath);
		return SerializationUtility.DeserializeValue<MyData>(bytes, DataFormat.Binary);
	}
}

public class MyData
{
	
}