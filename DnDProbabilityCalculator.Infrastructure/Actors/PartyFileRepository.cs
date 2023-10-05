using System.Text.Json;
using DnDProbabilityCalculator.Core;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Infrastructure.FileSystem;

namespace DnDProbabilityCalculator.Infrastructure.Actors;

public class PartyFileRepository : IPartyRepository
{
    private readonly IFileAccessor _fileAccessor;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public PartyFileRepository(IFileAccessor fileAccessor)
        => _fileAccessor = fileAccessor;

    public Party Get(string path)
    {
        var jsonString = _fileAccessor.ReadAllText(path);
        return JsonSerializer.Deserialize<Party>(jsonString, JsonSerializerOptions)
               ?? throw new FormatException(ErrorMessages.Wrong_File_Format);
    }

    public void Save(Party party, string filePath)
    {
        throw new NotImplementedException();
    }
}