using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;
using DnDProbabilityCalculator.Core.Adventuring.Attack;

namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public string Name { get; set; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();
    public int ProficiencyBonus { get; set; }
    public int ArmorClass { get; set; }

    public static INameStage New()
        => new NameStage(new());

    public double SavingThrowSuccessChance(AbilityScoreType abilityScoreType, int dc)
    {
        var abilityScore = AbilityScores.Get(abilityScoreType);
        var proficiencyBonus = abilityScore.IsProficient ? ProficiencyBonus : 0;
        return CalculateSavingThrowSuccessChance(dc, abilityScore, proficiencyBonus);
    }

    public GetHitProbability GetHitProbability(int attackModifier, int totalNumberOfAttacks, int numberOfHits)
    {
        GuardNumberOfAttacks(totalNumberOfAttacks);
        return Attack.GetHitProbability.Create(attackModifier, ArmorClass, totalNumberOfAttacks, numberOfHits);
    }

    private static double CalculateSavingThrowSuccessChance(int dc, AbilityScore abilityScore, int proficiencyBonus)
        => (21d + abilityScore.Modifier + proficiencyBonus - dc) / 20;

    private static void GuardNumberOfAttacks(int totalNumberOfAttacks)
    {
        if (totalNumberOfAttacks < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(totalNumberOfAttacks), ErrorMessages.Negative_Number_Of_Attacks);
        }
    }
}