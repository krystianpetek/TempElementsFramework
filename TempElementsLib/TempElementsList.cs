using TempElementsFramework_.Interfaces;

namespace TempElementsFramework_;

public class TempElementsList : ITempElements
{
    private readonly List<ITempElement> elements;

    public bool IsEmpty => elements.Count == 0;
    private bool disposed;

    public IReadOnlyCollection<ITempElement> Elements => elements;

    public TempElementsList()
    {
        elements = new List<ITempElement>();
    }

    public void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                foreach (var element in elements)
                {
                    DeleteElement(element);
                }
            }
            disposed = true;
        }
    }

    public T AddElement<T>() where T : ITempElement, new()
    {
        var temp = new T();
        elements.Add(temp);
        return temp;
    }

    public void RemoveDestroyed()
    {
        foreach (var element in elements)
        {
            if (!element.IsDestroyed)
                DeleteElement(element);
        }
    }

    public void MoveElementTo<T>(T element, string newPath) where T : ITempElement, new()
    {
        switch (element)
        {
            case TempDir tempDir:
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(tempDir.DirPath);
                    if (directoryInfo.Exists)
                    {
                        Directory.Move(directoryInfo.FullName, newPath);
                    }

                    break;
                }
            case TempFile tempFile:
                {
                    FileInfo fileInfo = new FileInfo(tempFile.FilePath);
                    if (fileInfo.Exists)
                    {
                        File.Move(fileInfo.FullName, newPath);
                    }

                    break;
                }
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~TempElementsList()
    {
        Dispose();
    }

    public void DeleteElement<T>(T element) where T : ITempElement
    {
        element.Dispose();
    }

}