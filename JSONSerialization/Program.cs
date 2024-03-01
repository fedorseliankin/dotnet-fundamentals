using System.Text.Json;

public class Employee
{
    public string EmployeeName { get; set; }
}

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


        var jsonString = JsonSerializer.Serialize(department);
        File.WriteAllText("MyFile.json", jsonString);
        var jsonStringFromFile = File.ReadAllText("MyFile.json");
        var deserializedDepartment = JsonSerializer.Deserialize<Department>(jsonStringFromFile);

        Console.WriteLine($"Department: {deserializedDepartment.DepartmentName}, Employee: {deserializedDepartment.Employees[0].EmployeeName}");
    }
}