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

    public Department DeepClone()
    {
        using (var memoryStream = new MemoryStream())
        {
            var formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, this);
            memoryStream.Position = 0;
            return (Department)formatter.Deserialize(memoryStream);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Department department1 = new Department
        {
            DepartmentName = "Sales",
            Employees = new List<Employee>
            {
                new Employee { EmployeeName = "John Doe" },
                new Employee { EmployeeName = "Jane Doe" }
            }
        };

        Department department2 = department1.DeepClone();
        department2.DepartmentName = "Marketing";
        department2.Employees[0].EmployeeName = "James Smith";

        Console.WriteLine(department1.DepartmentName);
        Console.WriteLine(department1.Employees[0].EmployeeName);

        Console.WriteLine(department2.DepartmentName);
        Console.WriteLine(department2.Employees[0].EmployeeName);
    }
}