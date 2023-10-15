using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;

namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public string Name { get; set; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();
    public int ProficiencyBonus { get; set; }

    // todo add armor class and to-hit calculation
    // todo add misc modifier somehow (+ something to modifier from e.g. aura)
    // todo connect to dnd beyond to get characters
    public static INameStage New()
        => new NameStage(new());

    public double DexteritySavingThrowSuccessChance(int dc)
        => SavingThrowSuccessChance(dc, AbilityScores.Dexterity);

    public double StrengthSavingThrowSuccessChance(int dc)
        => SavingThrowSuccessChance(dc, AbilityScores.Strength);

    public double ConstitutionSavingThrowSuccessChance(int dc)
        => SavingThrowSuccessChance(dc, AbilityScores.Constitution);

    public double IntelligenceSavingThrowSuccessChance(int dc)
        => SavingThrowSuccessChance(dc, AbilityScores.Intelligence);

    public double WisdomSavingThrowSuccessChance(int dc)
        => SavingThrowSuccessChance(dc, AbilityScores.Wisdom);

    public double CharismaSavingThrowSuccessChance(int dc)
        => SavingThrowSuccessChance(dc, AbilityScores.Charisma);

    private double SavingThrowSuccessChance(int dc, AbilityScore abilityScore)
    {
        var proficiencyBonus = abilityScore.IsProficient ? ProficiencyBonus : 0;
        return CalculateSuccessChance(dc, abilityScore, proficiencyBonus);
    }

    private static double CalculateSuccessChance(int dc, AbilityScore abilityScore, int proficiencyBonus)
        => (21d + abilityScore.Modifier + proficiencyBonus - dc) / 20;
}