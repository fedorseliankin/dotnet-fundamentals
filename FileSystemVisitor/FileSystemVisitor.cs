namespace FileSystemVisitor;

public class FileSystemVisitorCL
{
    private DirectoryInfo initDirectory;
    private Func<string, bool> filterFunc;

    public FileSystemVisitorCL(DirectoryInfo startDirectory)
    {
        initDirectory = startDirectory;
    }

    public FileSystemVisitorCL(
        DirectoryInfo startDirectory,
        Func<string, bool> filterFunc
    )
    {
        initDirectory = startDirectory;
        this.filterFunc = filterFunc;
    }

    public IEnumerable<string> getEntries()
    {
        return getEntries(initDirectory);
    }

    private IEnumerable<string> getEntries(DirectoryInfo currentDirectory)
    {
        foreach (var fileSystemInfo in currentDirectory.EnumerateFileSystemInfos())
        {
            if (fileSystemInfo is FileInfo file)
            {
                yield return processFile(file);
            }
            if (fileSystemInfo is DirectoryInfo directory)
            {
                yield return directory.Name;
                foreach (string item in getEntries(directory))
                {
                    yield return item;
                }
            }
        }
    }

    private string processFile(FileInfo fileInfo)
    {
        if (filterFunc == null || filterFunc(fileInfo.Name))
        {
            return fileInfo.Name;
        }
        return null;
    }
}

