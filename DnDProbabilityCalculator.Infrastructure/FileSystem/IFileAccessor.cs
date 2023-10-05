namespace DnDProbabilityCalculator.Infrastructure.FileSystem;

public interface IFileAccessor
{
    string ReadAllText(string path);
}