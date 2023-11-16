using FileSystemVisitor;

Console.WriteLine("Input directory path: ");
string path = Console.ReadLine();
//Console.WriteLine("Input filter by date: ");
//string filter = Console.ReadLine();
//new DirectoryInfo($@"{path}");
FileSystemVisitorCL fileSystemVisitor = new FileSystemVisitorCL(new DirectoryInfo($@"{path}"));
foreach (string item in fileSystemVisitor.getEntries())
{
    Console.WriteLine(item);
}