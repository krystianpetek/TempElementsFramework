using System.Text;
using TempElementsFramework_.Interfaces;

namespace TempElementsFramework_;

public class TempFile : ITempFile
{
    public string FilePath => fileInfo.FullName;
    public bool IsDestroyed => !fileInfo.Exists;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public TempFile(string fileName)
    {
        fileInfo = new FileInfo(fileName);
        fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    }

    public TempFile() : this(Path.GetTempFileName())
    {
    }

    public readonly FileStream fileStream;
    public readonly FileInfo fileInfo;

    ~TempFile()
    {
        Dispose(false);
    }

    public void Dispose(bool disposing)
    {
        if (disposing)
            fileStream?.Dispose();
        try
        {
            fileInfo?.Delete();
        }
        catch (IOException exception)
        {
            Console.WriteLine(exception);
        }
    }

    public void AddText(string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fileStream.Write(info, 0, info.Length);
        fileStream.Flush();
    }
}