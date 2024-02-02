using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class MyClass : ISerializable
{
    public int Num { get; set; }
    public string Str { get; set; }

    public MyClass() { }

    public MyClass(SerializationInfo info, StreamingContext context)
    {
        Num = info.GetInt32("Num");
        Str = info.GetString("Str");
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue("Num", Num);
        info.AddValue("Str", Str);
    }
}

class Program
{
    static void Main(string[] args)
    {
        MyClass myObject = new MyClass() { Num = 1, Str = "test" };

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write);

        formatter.Serialize(stream, myObject);
        stream.Close();

        stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read);
        MyClass deserializedObject = (MyClass)formatter.Deserialize(stream);

        Console.WriteLine(deserializedObject.Num);
        Console.WriteLine(deserializedObject.Str);
        Console.ReadKey();
    }
}