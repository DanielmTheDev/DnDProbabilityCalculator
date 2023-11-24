﻿using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class ActorTests
{
    [TestMethod]
    [DataRow(1, 6, 16, 2, 8.5)]
    [DataRow(2, 8, 14, 5, 16)]
    [DataRow(3, 10, 10, 2, 18.5)]
    [DataRow(1, 12, 6, 1, 5.5)]
    [DataRow(4, 4, 12, 6, 17)]
    [DataRow(1, 4, 10, 0, 2.5)]
    public void AverageDamage_WhenCalled_ReturnsDamage(int numberOfDice, int diceSides, int abilityScore, int bonus, double expectedDamage)
    {
        // Arrange
        var actor = new Actor
        {
            Name = "Durak",
            AbilityScores = new()
            {
                Dexterity = 11,
                Strength = abilityScore,
                Constitution = 10,
                Intelligence = 9,
                Wisdom = 13,
                Charisma = 10
            },
            ProficiencyBonus = 2,
            ArmorClass = 15,
            NumberOfAttacks = 2,
            AttackAbility = AbilityScoreType.Strength,
            Weapon = new()
            {
                NumberOfDice = numberOfDice,
                DiceSides = diceSides,
                Bonus = bonus
            }
        };

        // Act and Assert
        Assert.AreEqual(expectedDamage, actor.AverageDamagePerHit);
    }

    [TestMethod]
    public void CalculateSavingThrowSuccessChance_WithoutProficiency_ReturnsChanceForSuccess()
    {
        // Arrange
        var actor = BuildValidActor();

        // Act
        var dexterityChance = actor.SavingThrowSuccessChance(AbilityScoreType.Dexterity, 13);
        var charismaChance = actor.SavingThrowSuccessChance(AbilityScoreType.Charisma, 13);
        var intelligenceChance = actor.SavingThrowSuccessChance(AbilityScoreType.Intelligence, 13);

        // Assert
        Assert.AreEqual(0.4, dexterityChance);
        Assert.AreEqual(0.4, charismaChance);
        Assert.AreEqual(0.35, intelligenceChance);
    }

    [TestMethod]
    public void CalculateSavingThrowSuccessChance_WithProficiency_ReturnsChanceForSuccess()
    {
        // Arrange
        var actor = BuildValidActor();

        // Act
        var strengthChance = actor.SavingThrowSuccessChance(AbilityScoreType.Strength, 16);
        var constChance = actor.SavingThrowSuccessChance(AbilityScoreType.Constitution, 16);

        // Assert
        Assert.AreEqual(0.5, strengthChance);
        Assert.AreEqual(0.45, constChance);
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(-1)]
    public void ReceiveHitChance_WithNegativeNumberOfAttacks_ThrowsArgumentException(int numberOfAttacks)
    {
        // Arrange
        var actor = new Actor
        {
            Name = "Durak",
            AbilityScores = new()
            {
                Dexterity = 12,
                Strength = new()
                {
                    Value = 13,
                    IsProficient = true
                },
                Constitution = new()
                {
                    Value = 10,
                    IsProficient = true
                },
                Intelligence = 16,
                Wisdom = 15,
                Charisma = 8
            },
            ProficiencyBonus = 4,
            ArmorClass = 15,
            NumberOfAttacks = 2,
            AttackAbility = AbilityScoreType.Dexterity,
            Weapon = new()
            {
                NumberOfDice = 1,
                DiceSides = 6,
                Bonus = 0
            }
        };


        // Act and Assert
        var message = Assert.ThrowsException<ArgumentOutOfRangeException>(() => actor.ReceiveHitChance(6, numberOfAttacks, 2));
        Assert.IsTrue(message.Message.Contains(ErrorMessages.Negative_Number_Of_Attacks));
    }

    [TestMethod]
    [DataRow(1, 0.42)]
    [DataRow(2, 0.49)]
    public void CalculateDeliverHitChance_WhenCalled_ReturnsChanceForSuccess(int numberOfHits, double expectedProbability)
    {
        // Arrange
        var actor = new Actor
        {
            Name = "Durak",
            AbilityScores = new()
            {
                Dexterity = 11,
                Strength = new() { Value = 16, IsProficient = true },
                Constitution = new() { Value = 10, IsProficient = true },
                Intelligence = 9,
                Wisdom = 13,
                Charisma = 10
            },
            ProficiencyBonus = 2,
            ArmorClass = 15,
            NumberOfAttacks = 2,
            AttackAbility = AbilityScoreType.Strength,
            Weapon = new()
            {
                NumberOfDice = 1,
                DiceSides = 6,
                Bonus = 2
            }
        };

        // Act
        var chance = actor.DeliverHitChance(14, numberOfHits);

        // Assert
        Assert.AreEqual(expectedProbability, chance.Probability);
    }

    private static Actor BuildValidActor()
        => new()
        {
            Name = "Durak",
            AbilityScores = new()
            {
                Dexterity = 11,
                Strength = new() { Value = 13, IsProficient = true },
                Constitution = new() { Value = 10, IsProficient = true },
                Intelligence = 9,
                Wisdom = 13,
                Charisma = 10
            },
            ProficiencyBonus = 4,
            ArmorClass = 5,
            NumberOfAttacks = 2,
            AttackAbility = AbilityScoreType.Dexterity,
            Weapon = new()
            {
                NumberOfDice = 10,
                DiceSides = 6,
                Bonus = 0
            }
        };
}