namespace BreakTheCode.Interfaces
{
    public interface IReader<T>
    {
        T Read(string filename);
    }
}
