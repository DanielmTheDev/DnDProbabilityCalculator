using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Core.Adventuring.Probabilities;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class ActorTests
{
    [TestMethod]
    public void FluentBuilder_WithValidValues_ReturnsFullyBuiltActor()
    {
        // Arrange and Act
        var actor = BuildValidActor();

        // Assert
        Assert.AreEqual("Durak", actor.Name);
        Assert.AreEqual(13, actor.AbilityScores.Strength);
        Assert.IsTrue(actor.AbilityScores.Strength.IsProficient);
        Assert.AreEqual(11, actor.AbilityScores.Dexterity);
        Assert.AreEqual(10, actor.AbilityScores.Constitution);
        Assert.IsTrue(actor.AbilityScores.Constitution.IsProficient);
        Assert.AreEqual(13, actor.AbilityScores.Wisdom);
        Assert.AreEqual(9, actor.AbilityScores.Intelligence);
        Assert.AreEqual(10, actor.AbilityScores.Charisma);
        Assert.AreEqual(5, actor.ArmorClass);
        Assert.AreEqual(4, actor.ProficiencyBonus);
    }

    [TestMethod]
    [DataRow(-1)]
    [DataRow(31)]
    public void FluentBuilder_WithTooLowAbilityScore_ThrowsException(int abilityScore)
    {
        // Arrange Act Assert
        var message = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            Actor
                .New()
                .WithName("Durak")
                .WithStrength(abilityScore, true)
                .WithDexterity(11)
                .WithConstitution(10, true)
                .WithWisdom(13)
                .WithIntelligence(11)
                .WithCharisma(10)
                .WithProficiency(4)
                .WithArmorClass(5)
                .Build()).Message;
        Assert.IsTrue(message.Contains(ErrorMessages.Ability_Score_Out_Of_Range));
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
    public void CalculateHitProbabilities_WithNegativeNumberOfAttacks_ThrowsArgumentException(int numberOfAttacks)
    {
        // Arrange
        var actor = Actor
            .New()
            .WithName("Durak")
            .WithStrength(13, true)
            .WithDexterity(11)
            .WithConstitution(10, true)
            .WithWisdom(13)
            .WithIntelligence(9)
            .WithCharisma(10)
            .WithProficiency(4)
            .WithArmorClass(15)
            .Build();

        // Act and Assert
        var message = Assert.ThrowsException<ArgumentOutOfRangeException>(() => actor.CalculateGetHitProbabilities(6, numberOfAttacks));
        Assert.IsTrue(message.Message.Contains(ErrorMessages.Negative_Number_Of_Attacks));
    }

    private static Actor BuildValidActor()
        => Actor
            .New()
            .WithName("Durak")
            .WithStrength(13, true)
            .WithDexterity(11)
            .WithConstitution(10, true)
            .WithWisdom(13)
            .WithIntelligence(9)
            .WithCharisma(10)
            .WithProficiency(4)
            .WithArmorClass(5)
            .Build();
}