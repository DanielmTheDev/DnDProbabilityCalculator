using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Core.Adventuring.Attack;

namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public required string Name { get; init; } = string.Empty;
    public required int ProficiencyBonus { get; init; }
    public required int ArmorClass { get; init; }
    public required int NumberOfAttacks { get; init; }
    public required AbilityScores AbilityScores { get; init; }
    public required AbilityScoreType AttackAbility { get; init; }
    public required Weapon Weapon { get; init; }

    public double AverageDamagePerHit
    {
        get
        {
            var abilityModifier = AbilityScores.AsList().Single(score => score.Type == AttackAbility).Modifier;
            var weaponModifier = Weapon.Bonus;
            var miscDamageBonus = Weapon.MiscDamageBonus;
            return (Weapon.DiceSides + 1) / 2.0 * Weapon.NumberOfDice + abilityModifier + weaponModifier + miscDamageBonus;
        }
    }

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
        var abilityModifier = AbilityScores.AsList().Single(score => score.Type == AttackAbility).Modifier;
        var weaponModifier = Weapon.Bonus;
        var totalModifier = abilityModifier + weaponModifier + ProficiencyBonus;
        return HitChance.Create(totalModifier, armorClass, NumberOfAttacks, numberOfHits);
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