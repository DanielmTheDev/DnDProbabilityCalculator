using System.Text.Json;
using DnDProbabilityCalculator.Core;

namespace DnDProbabilityCalculator;

public class Character
{
    public readonly Attributes Attributes = new();

    public static IEnumerable<Character> FromJsonString(string jsonString)
        => JsonSerializer.Deserialize<IEnumerable<Character>>(jsonString) ?? throw new FormatException(ErrorMessages.Wrong_File_Format);
}