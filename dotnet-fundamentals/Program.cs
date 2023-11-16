using FileSystemVisitor;

Console.WriteLine("Input directory path: ");
string path = Console.ReadLine();
FileSystemVisitorCL fileSystemVisitor = new FileSystemVisitorCL(new DirectoryInfo($@"{path}"));
foreach (string item in fileSystemVisitor.getEntries())
{
    Console.WriteLine(item);
}