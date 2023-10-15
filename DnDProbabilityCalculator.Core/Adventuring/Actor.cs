using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public string Name { get; set; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();
    public int ProficiencyBonus { get; set; }
    public int ArmorClass { get; set; }

    // todo add armor class and to-hit calculation (maybe one row for each 1, 2x attack, 3x attack etc)
    // todo add misc modifier somehow (+ something to modifier from e.g. aura)
    // todo connect to dnd beyond to get characters
    // todo advantage/disadvantage (maybe when pressing A/D, tables change to reflect advantage/disadvantage)
    public static INameStage New()
        => new NameStage(new());

    public double SavingThrowSuccessChance(AbilityScoreType abilityScoreType, int dc)
    {
        var abilityScore = AbilityScores.Get(abilityScoreType);
        var proficiencyBonus = abilityScore.IsProficient ? ProficiencyBonus : 0;
        return CalculateSuccessChance(dc, abilityScore, proficiencyBonus);
    }

    public double GetHitChance(int modifier)
        => (21d + modifier - ArmorClass) / 20;

    private static double CalculateSuccessChance(int dc, AbilityScore abilityScore, int proficiencyBonus)
        => (21d + abilityScore.Modifier + proficiencyBonus - dc) / 20;
}