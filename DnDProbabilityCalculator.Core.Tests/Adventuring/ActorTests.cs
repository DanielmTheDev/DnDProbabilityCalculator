using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class ActorTests
{
    [TestMethod]
    [DataRow(1, 6, 16, 6.5)]
    [DataRow(2, 8, 14, 11)]
    [DataRow(3, 10, 10, 16.5)]
    [DataRow(1, 12, 6, 4.5)]
    [DataRow(4, 4, 12, 11)]
    [DataRow(1, 4, 10, 2.5)]
    public void AverageDamage_WhenCalled_ReturnsDamage(int numberOfDice, int diceSize, int abilityScore, double expectedDamage)
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
            WeaponDamage = new(numberOfDice, diceSize)
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
            WeaponDamage = new(1, 6)
        };


        // Act and Assert
        var message = Assert.ThrowsException<ArgumentOutOfRangeException>(() => actor.ReceiveHitChance(6, numberOfAttacks, 2));
        Assert.IsTrue(message.Message.Contains(ErrorMessages.Negative_Number_Of_Attacks));
    }

    [TestMethod]
    [DataRow(1, 0.48)]
    [DataRow(2, 0.36)]
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
            WeaponDamage = new(1, 6)
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
            WeaponDamage = new(1, 6)
        };
}