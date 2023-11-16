namespace FileSystemVisitor;

public class FileSystemVisitorCL
{
    private DirectoryInfo initDirectory;

    public FileSystemVisitorCL(DirectoryInfo startDirectory)
    {
        initDirectory = startDirectory;
    }

    public IEnumerable<string> getEntries()
    {
        return getEntries(initDirectory);
    }

    public IEnumerable<string> getEntries(DirectoryInfo currentDirectory)
    {
        foreach (var fileSystemInfo in currentDirectory.EnumerateFileSystemInfos())
        {
            if (fileSystemInfo is FileInfo file)
            {
                yield return file.Name;
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
}

