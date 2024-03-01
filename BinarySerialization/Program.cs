using System.Runtime.Serialization.Formatters.Binary;

[Serializable]
public class Employee
{
    public string EmployeeName { get; set; }
}

[Serializable]
public class Department
{
    public string DepartmentName { get; set; }
    public List<Employee> Employees { get; set; }
}

public class Program
{
    public static void Main()
    {
        var department = new Department
        {
            DepartmentName = "Sales",
            Employees = new List<Employee>()
            {
                new Employee { EmployeeName = "John Doe" },
                new Employee { EmployeeName = "Jane Doe" }
            }
        };

        var formatter = new BinaryFormatter();

        using (Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None))
        {
            formatter.Serialize(stream, department);
        }

        Department deserializedDepartment;

        using (Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            deserializedDepartment = (Department)formatter.Deserialize(stream);
        }

        Console.WriteLine($"Department: {deserializedDepartment.DepartmentName}, Employee: {deserializedDepartment.Employees[0].EmployeeName}");
    }
}
