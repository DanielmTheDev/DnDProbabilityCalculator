using System.Text.Json;
using DnDProbabilityCalculator.Core;

namespace DnDProbabilityCalculator;

public class Character
{
    public string Name { get; set; } = string.Empty;
    public Attributes Attributes { get; set; } = new();

    public static IEnumerable<Character> FromJsonString(string jsonString)
        => JsonSerializer.Deserialize<IEnumerable<Character>>(jsonString, new JsonSerializerOptions(JsonSerializerDefaults.Web))
           ?? throw new FormatException(ErrorMessages.Wrong_File_Format);
}