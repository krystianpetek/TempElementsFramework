using TempElementsFramework_.Interfaces;

namespace TempElementsFramework_;

public class TempDir : ITempDir
{
    private DirectoryInfo dirInfo;
    public string DirPath => dirInfo.FullName;
    public bool IsEmpty => (dirInfo.GetDirectories().Length + dirInfo.GetFiles().Length) == 0;

    public bool IsDestroyed => !dirInfo.Exists;

    public void Dispose()
    {
        try
        {
            dirInfo?.Delete();
        }
        catch (IOException exception)
        {
            Console.WriteLine(exception);
        }
    }

    public void Empty()
    {
        throw new NotImplementedException();
    }

    public TempDir() : this(Guid.NewGuid().ToString())
    { }

    public TempDir(string name)
    {
        var dirPath = Path.Combine(Path.GetTempPath(), name);
        dirInfo = new DirectoryInfo(dirPath);
        Directory.CreateDirectory(dirInfo.FullName);
    }

    ~TempDir()
    {
        Dispose();
    }
}