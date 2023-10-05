namespace DnDProbabilityCalculator.Infrastructure.FileSystem;

public class FileAccessor : IFileAccessor
{
    public string ReadAllText(string path)
        => File.ReadAllText(path);
}