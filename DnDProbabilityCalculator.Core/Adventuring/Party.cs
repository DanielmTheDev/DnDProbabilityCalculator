using System.Text.Json;

namespace DnDProbabilityCalculator.Core;

public class Party
{
    private static readonly JsonSerializerOptions JsonSerializerOptions = new(JsonSerializerDefaults.Web);

    public static IEnumerable<Character> FromJsonString(string jsonString)
        => JsonSerializer.Deserialize<IEnumerable<Character>>(jsonString, JsonSerializerOptions)
           ?? throw new FormatException(ErrorMessages.Wrong_File_Format);
}