namespace TempElementsFramework_.Interfaces;

public interface ITempElement : IDisposable
{
    public bool IsDestroyed { get; }
}