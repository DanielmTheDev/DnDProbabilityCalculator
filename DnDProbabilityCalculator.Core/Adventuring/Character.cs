namespace DnDProbabilityCalculator.Core.Adventuring;

public class Character
{
    public string Name { get; init; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();

    // todo: saving throws
}