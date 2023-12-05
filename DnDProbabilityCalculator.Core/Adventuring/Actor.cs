﻿using DnDProbabilityCalculator.Core.Adventuring.Abilities;
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

    public double SavingThrowSuccessChance(AbilityScoreType abilityScoreType, int dc, AdvantageType advantage)
    {
        var abilityScore = AbilityScores.Get(abilityScoreType);
        var proficiencyBonus = abilityScore.IsProficient
            ? ProficiencyBonus
            : 0;
        var positiveModifer = proficiencyBonus + abilityScore.Modifier;
        return Probability.Calculate(positiveModifer, dc, advantage);
    }

    public HitChance DeliverHitChance(int armorClass, int numberOfHits, AdvantageType advantage)
    {
        var abilityModifier = AbilityScores.AsList().Single(score => score.Type == AttackAbility).Modifier;
        var weaponModifier = Weapon.Bonus;
        var totalModifier = abilityModifier + weaponModifier + ProficiencyBonus;
        return HitChance.Create(totalModifier, armorClass, NumberOfAttacks, numberOfHits, advantage);
    }

    public HitChance ReceiveHitChance(int attackModifier, int totalNumberOfAttacks, int numberOfHits, AdvantageType advantage)
    {
        GuardNumberOfAttacks(totalNumberOfAttacks);
        return HitChance.Create(attackModifier, ArmorClass, totalNumberOfAttacks, numberOfHits, advantage);
    }

    private static void GuardNumberOfAttacks(int totalNumberOfAttacks)
    {
        if (totalNumberOfAttacks < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(totalNumberOfAttacks), ErrorMessages.Negative_Number_Of_Attacks);
        }
    }
}