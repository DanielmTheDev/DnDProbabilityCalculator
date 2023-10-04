namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public string Name { get; init; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();

    // todo: saving throw probability (probably parameterized)
    // todo: builder to create a character
}