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
            .Build();

        // Assert
        Assert.AreEqual(13, actor.AbilityScores.Strength);
        Assert.IsTrue(actor.AbilityScores.Strength.IsProficient);
        Assert.AreEqual(11, actor.AbilityScores.Dexterity);
        Assert.AreEqual(10, actor.AbilityScores.Constitution);
        Assert.IsTrue(actor.AbilityScores.Constitution.IsProficient);
        Assert.AreEqual(13, actor.AbilityScores.Wisdom);
        Assert.AreEqual(11, actor.AbilityScores.Intelligence);
        Assert.AreEqual(10, actor.AbilityScores.Charisma);
    }
}