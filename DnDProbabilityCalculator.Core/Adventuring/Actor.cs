using DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public string Name { get; init; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();
    public int ProficiencyBonus { get; set; }

    // todo add armor class and to-hit calculation
    public static IStrengthStage New()
        => new StrengthStage(new Actor());

    public double CalculateSavingThrowSuccessChance(int dc, AbilityType abilityType)
    {
        var modifier = AbilityScores.GetModifierOf(abilityType);
        return (21d + modifier - dc) / 20;
    }
}