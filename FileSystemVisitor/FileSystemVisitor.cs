namespace FileSystemVisitor;

public enum CheckResult
{
    Contains,
    Equals,
    No
}

public enum AbortStatus
{
    No,
    Wait,
    Yes,
}

public class FileSystemVisitorCL
{
    private class FindedItemEvent: EventArgs
    {
        public string Name { get; set; }
    }
    private readonly DirectoryInfo initDirectory;
    private readonly Func<string, CheckResult>? filterFunc;
    private AbortStatus? abort;
    private string? show;

    private readonly EventHandler<EventArgs> Start = (s, e) =>
    {
        Console.WriteLine("Search started!");
    };

    private readonly EventHandler<EventArgs> Finish = (s, e) =>
    {
        Console.WriteLine("Search finished!");
    };

    private readonly EventHandler<FindedItemEvent> FileFound = (s, e) =>
    {
        Console.WriteLine("File Found: " + e.Name);
    };

    private readonly EventHandler<FindedItemEvent> DirFound = (s, e) =>
    {
        Console.WriteLine("Directory Found: " + e.Name);
    };

    public FileSystemVisitorCL(DirectoryInfo startDirectory)
    {
        initDirectory = startDirectory;
        abort = AbortStatus.No;
        show = "all";
    }

    public FileSystemVisitorCL(
        DirectoryInfo startDirectory,
        Func<string, CheckResult>? filterFunc,
        string? show,
        string? abort
    ) : this(startDirectory)
    {
        this.filterFunc = filterFunc;
        if (abort is "y")
        {
            this.abort = AbortStatus.Wait;
        } else if (abort is "n")
        {
            this.abort = AbortStatus.No;
        } else
        {
            this.abort = AbortStatus.No;
        }
        this.show = show;
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
                if (name != null && (show == "all" || show == "f"))
                {
                    OnEvent(FileFound, new FindedItemEvent() {Name = name});
                    yield return name;
                }
                if (abort == AbortStatus.Yes)
                {
                    break;
                }
            }
            if (fileSystemInfo is DirectoryInfo directory)
            {
                var name = CheckName(directory);
                if (name != null && (show == "all" || show == "d"))
                {
                    OnEvent(DirFound, new FindedItemEvent() { Name = name });
                    yield return name;
                    if (abort == AbortStatus.Yes)
                    {
                        break;
                    }
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
        CheckResult checkResult = filterFunc(fileInfo.Name);
        if (checkResult == CheckResult.Equals)
        {
            this.abort = AbortStatus.Yes;
        }
        if (filterFunc == null || checkResult != CheckResult.No)
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

