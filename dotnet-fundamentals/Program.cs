using FileSystemVisitor;

Console.WriteLine("Input directory path: ");
string path = Console.ReadLine();
Console.WriteLine("Input filtering string");
string filter = Console.ReadLine();
FileSystemVisitorCL fileSystemVisitor = new(
    new DirectoryInfo($@"{path}"),
    filter is null or "" ? null : (string name) => name.Contains(filter)
);

foreach (string item in fileSystemVisitor.getEntries())
{
    Console.WriteLine(item);
}