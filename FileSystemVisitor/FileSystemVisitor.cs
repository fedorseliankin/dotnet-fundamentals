namespace FileSystemVisitor;


public class FileSystemVisitorCL
{
    private class FindedItemEvent: EventArgs
    {
        public string Name { get; set; }
    }
    private readonly DirectoryInfo initDirectory;
    private readonly Func<string, bool>? filterFunc;

    private static readonly EventHandler<EventArgs> Start = (s, e) =>
    {
        Console.WriteLine("Search started!");
    };

    private static readonly EventHandler<EventArgs> Finish = (s, e) =>
    {
        Console.WriteLine("Search finished!");
    };

    private static readonly EventHandler<FindedItemEvent> FileFound = (s, e) =>
    {
        Console.WriteLine("File Found: " + e.Name);
    };

    private static readonly EventHandler<FindedItemEvent> DirFound = (s, e) =>
    {
        Console.WriteLine("Directory Found: " + e.Name);
    };

    public FileSystemVisitorCL(DirectoryInfo startDirectory)
    {
        initDirectory = startDirectory;
    }

    public FileSystemVisitorCL(
        DirectoryInfo startDirectory,
        Func<string, bool>? filterFunc
    ) : this(startDirectory)
    {
        this.filterFunc = filterFunc;
    }

    public IEnumerable<string> GetEntries()
    {
        OnEvent(Start, new EventArgs());
        foreach (var elem in GetEntries(initDirectory))
        {
            yield return elem;
        }
        OnEvent(Finish, new EventArgs());
    }

    private IEnumerable<string> GetEntries(DirectoryInfo currentDirectory)
    {
        foreach (var fileSystemInfo in currentDirectory.EnumerateFileSystemInfos())
        {
            if (fileSystemInfo is FileInfo file)
            {
                var name = CheckName(file);
                if (name != null)
                {
                    OnEvent(FileFound, new FindedItemEvent() {Name = name});
                    yield return name;
                }
            }
            if (fileSystemInfo is DirectoryInfo directory)
            {
                var name = CheckName(directory);
                if (name != null)
                {
                    OnEvent(DirFound, new FindedItemEvent() { Name = name });
                    yield return name;
                }
                foreach (string item in GetEntries(directory))
                {
                    yield return item;
                }
            }
        }
    }

    private string? CheckName(FileSystemInfo fileInfo)
    {
        if (filterFunc == null || filterFunc(fileInfo.Name))
        {
            return fileInfo.Name;
        }
        return null;
    }

    private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
    {
        someEvent?.Invoke(this, args);
    }
}

