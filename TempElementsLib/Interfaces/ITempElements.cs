namespace TempElementsFramework_.Interfaces;

public interface ITempElements : IDisposable
{
    public bool IsEmpty { get; }
    public IReadOnlyCollection<ITempElement> Elements { get; }
    public T AddElement<T>() where T : ITempElement, new();
    public void RemoveDestroyed();
}