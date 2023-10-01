using System.Text.Json;

namespace DnDProbabilityCalculator.Core.Adventuring;

public class Party
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public IList<Character> Characters { get; set; } = new List<Character>();

    public static Party FromJsonString(string jsonString)
        => JsonSerializer.Deserialize<Party>(jsonString, JsonSerializerOptions)
           ?? throw new FormatException(ErrorMessages.Wrong_File_Format);
}