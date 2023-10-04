using DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public string Name { get; init; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();
    public int ProficiencyBonus { get; set; }

    // todo: saving throw probability (probably parameterized)
    // todo: builder to create a character
    public static IStrengthStage New()
        => new StrengthStage(new Actor());
}