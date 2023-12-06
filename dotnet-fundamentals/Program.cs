using FileSystemVisitor;

Console.WriteLine("Input directory path: ");
string path = Console.ReadLine();
Console.WriteLine("Input filtering string");
string filter = Console.ReadLine();
Console.WriteLine("Should abort on first fullmatch found? (y/n)");
string shouldAbort = Console.ReadLine();
Console.WriteLine("Should show files/directories/all items found? (f/d/all)");
string show = Console.ReadLine();
FileSystemVisitorCL fileSystemVisitor = new(
    new DirectoryInfo($@"{path}"),
    filter is null or "" ? null : (string name) => {
        if (name.Contains(filter)) {
            if (name.Equals(filter))
            {
                return CheckResult.Equals;
            }
            return CheckResult.Contains;
        }
        return CheckResult.No;
    },
    show is null ? "" : show,
    shouldAbort is null ? "n" : shouldAbort
);

foreach (string item in fileSystemVisitor.GetEntries())
{
    Console.WriteLine(item);
}