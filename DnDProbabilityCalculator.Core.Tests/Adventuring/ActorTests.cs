using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class ActorTests
{
    [TestMethod]
    public void FluentBuilder_WithValidValues_ReturnsFullyBuiltActor()
    {
        // Arrange and Act
        var actor = Actor
            .New()
            .WithStrength(13, true)
            .WithDexterity(11)
            .WithConstitution(10, true)
            .WithWisdom(13)
            .WithIntelligence(11)
            .WithCharisma(10)
            .WithProficiency(4)
            .Build();

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
        Assert.AreEqual(11, actor.AbilityScores.Intelligence);
        Assert.AreEqual(AbilityType.Intelligence, actor.AbilityScores.Intelligence.Type);
        Assert.AreEqual(10, actor.AbilityScores.Charisma);
        Assert.AreEqual(AbilityType.Charisma, actor.AbilityScores.Charisma.Type);
        Assert.AreEqual(4, actor.ProficiencyBonus);
    }
}