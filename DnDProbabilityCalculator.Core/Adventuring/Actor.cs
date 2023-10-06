using DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public string Name { get; set; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();
    public int ProficiencyBonus { get; set; }

    // todo add armor class and to-hit calculation
    // todo add misc modifier somehow (+ something to modifier from e.g. aura)
    public static INameStage New()
        => new NameStage(new());

    public double CalculateSavingThrowSuccessChance(int dc, AbilityType abilityType)
    {
        var abilityScore = AbilityScores.GetScoreOfType(abilityType);
        var proficiencyBonus = abilityScore.IsProficient ? ProficiencyBonus : 0;
        return CalculateSuccessChance(dc, abilityScore, proficiencyBonus);
    }

    private static double CalculateSuccessChance(int dc, AbilityScore abilityScore, int proficiencyBonus)
        => (21d + abilityScore.Modifier + proficiencyBonus - dc) / 20;
}