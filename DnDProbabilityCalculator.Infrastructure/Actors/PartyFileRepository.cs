using System.Text.Json;
using System.Text.Json.Serialization;
using DnDProbabilityCalculator.Core;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Infrastructure.FileSystem;
using DnDProbabilityCalculator.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace DnDProbabilityCalculator.Infrastructure.Actors;

public class PartyFileRepository(IFileAccessor fileAccessor, IOptions<FileRepositoryOptions> options) : IPartyRepository
{
    private readonly FileRepositoryOptions _options = options.Value;

    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public Party Get()
    {
        var jsonString = fileAccessor.ReadAllText(_options.FilePath);
        return JsonSerializer.Deserialize<Party>(jsonString, JsonSerializerOptions)
               ?? throw new FormatException(ErrorMessages.Wrong_File_Format);
    }
}