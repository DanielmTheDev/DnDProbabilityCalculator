using System.Text.Json;
using DnDProbabilityCalculator.Core;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Infrastructure.FileSystem;
using DnDProbabilityCalculator.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace DnDProbabilityCalculator.Infrastructure.Actors;

public class PartyFileRepository : IPartyRepository
{
    private readonly IFileAccessor _fileAccessor;
    private readonly FileRepositoryOptions _options;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public PartyFileRepository(IFileAccessor fileAccessor, IOptions<FileRepositoryOptions> options)
    {
        _fileAccessor = fileAccessor;
        _options = options.Value;
    }

    public Party Get()
    {
        var jsonString = _fileAccessor.ReadAllText(_options.FilePath);
        return JsonSerializer.Deserialize<Party>(jsonString, JsonSerializerOptions)
               ?? throw new FormatException(ErrorMessages.Wrong_File_Format);
    }

    public void Save(Party party)
    {
        throw new NotImplementedException();
    }
}