using DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public string Name { get; init; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();
    public int ProficiencyBonus { get; set; }

    // todo add armor class and to-hit calculation
    // todo add misc modifier somehow (+ something to modifier from e.g. aura)
    public static IStrengthStage New()
        => new StrengthStage(new Actor());

    public double CalculateSavingThrowSuccessChance(int dc, AbilityType abilityType)
    {
        var abilityScore = AbilityScores.GetScoreOfType(abilityType);
        var proficiencyBonus = abilityScore.IsProficient ? ProficiencyBonus : 0;
        return (21d + abilityScore.Modifier + proficiencyBonus - dc) / 20;
    }
}