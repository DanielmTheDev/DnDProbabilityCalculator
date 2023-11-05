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
    public int NumberOfAttacks { get; set; }
    public AbilityScoreType AttackAbility { get; set; }

    public static INameStage New()
        => new NameStage(new());

    public double SavingThrowSuccessChance(AbilityScoreType abilityScoreType, int dc)
    {
        var abilityScore = AbilityScores.Get(abilityScoreType);
        var proficiencyBonus = abilityScore.IsProficient
            ? ProficiencyBonus
            : 0;
        return CalculateSavingThrowSuccessChance(dc, abilityScore, proficiencyBonus);
    }

    public HitChance DeliverHitChance(int armorClass, int numberOfHits)
    {
        var attackModifier = ProficiencyBonus + AbilityScores.AsList().Single(score => score.Type == AttackAbility).Modifier;
        return HitChance.Create(attackModifier, armorClass, NumberOfAttacks, numberOfHits);
    }

    public HitChance ReceiveHitChance(int attackModifier, int totalNumberOfAttacks, int numberOfHits)
    {
        GuardNumberOfAttacks(totalNumberOfAttacks);
        return HitChance.Create(attackModifier, ArmorClass, totalNumberOfAttacks, numberOfHits);
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