using System.Xml.Serialization;

public class Employee
{
    [XmlElement(ElementName = "Name")]
    public string EmployeeName { get; set; }
}

public class Department
{
    [XmlAttribute(AttributeName = "DeptName")]
    public string DepartmentName { get; set; }

    [XmlArray("Employees")]
    [XmlArrayItem("Employee")]
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

        var formatter = new XmlSerializer(typeof(Department));

        using (Stream stream = new FileStream("MyFile.xml", FileMode.Create, FileAccess.Write, FileShare.None))
        {
            formatter.Serialize(stream, department);
        }

        Department deserializedDepartment;

        using (Stream stream = new FileStream("MyFile.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
        {
            deserializedDepartment = (Department)formatter.Deserialize(stream);
        }

        Console.WriteLine($"Department: {deserializedDepartment.DepartmentName}, Employee: {deserializedDepartment.Employees[0].EmployeeName}");
    }
}