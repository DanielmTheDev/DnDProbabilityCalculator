using DnDProbabilityCalculator.Core.Adventuring;

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
        Assert.AreEqual(13, actor.AbilityScores.Strength);
        Assert.AreEqual(AbilityType.Strength, actor.AbilityScores.Strength.Type);
        Assert.IsTrue(actor.AbilityScores.Strength.IsProficient);
        Assert.AreEqual(11, actor.AbilityScores.Dexterity);
        Assert.AreEqual(AbilityType.Dexterity, actor.AbilityScores.Dexterity.Type);
        Assert.AreEqual(10, actor.AbilityScores.Constitution);
        Assert.AreEqual(AbilityType.Constitution, actor.AbilityScores.Constitution.Type);
        Assert.IsTrue(actor.AbilityScores.Constitution.IsProficient);
        Assert.AreEqual(13, actor.AbilityScores.Wisdom);
        Assert.AreEqual(AbilityType.Wisdom, actor.AbilityScores.Wisdom.Type);
        Assert.AreEqual(9, actor.AbilityScores.Intelligence);
        Assert.AreEqual(AbilityType.Intelligence, actor.AbilityScores.Intelligence.Type);
        Assert.AreEqual(10, actor.AbilityScores.Charisma);
        Assert.AreEqual(AbilityType.Charisma, actor.AbilityScores.Charisma.Type);
        Assert.AreEqual(4, actor.ProficiencyBonus);
    }

    private static Actor BuildValidActor()
        => Actor
            .New()
            .WithStrength(13, true)
            .WithDexterity(11)
            .WithConstitution(10, true)
            .WithWisdom(13)
            .WithIntelligence(9)
            .WithCharisma(10)
            .WithProficiency(4)
            .Build();

    [TestMethod]
    [DataRow(-1)]
    [DataRow(31)]
    public void FluentBuilder_WithTooLowAbilityScore_ThrowsException(int abilityScore)
    {
        // Arrange Act Assert
        var message = Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
            Actor
                .New()
                .WithStrength(abilityScore, true)
                .WithDexterity(11)
                .WithConstitution(10, true)
                .WithWisdom(13)
                .WithIntelligence(11)
                .WithCharisma(10)
                .WithProficiency(4)
                .Build()).Message;
        Assert.IsTrue(message.Contains(ErrorMessages.Ability_Score_Out_Of_Range));
    }

    [TestMethod]
    [DataRow(AbilityType.Dexterity, 13, 0.4)]
    [DataRow(AbilityType.Charisma, 12, 0.45)]
    [DataRow(AbilityType.Intelligence, 15, 0.25)]
    public void CalculateSavingThrowSuccessChance_WithoutProficiency_ReturnsChanceForSuccess(AbilityType abilityType, int dc, double expectedChance)
    {
        // Arrange
        var actor = BuildValidActor();

        // Act
        var successChance = actor.CalculateSavingThrowSuccessChance(dc, abilityType);

        // Assert
        Assert.AreEqual(expectedChance, successChance);
    }
}