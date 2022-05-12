namespace TempElementsFramework_.Interfaces;

public interface ITempDir : ITempElement
{
    public string DirPath { get; }
    public bool IsEmpty { get; }
    public void Empty();
}