using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class AbilityScoresTests
{
    [TestMethod]
    [DataRow(AbilityType.Strength, 8, -1)]
    [DataRow(AbilityType.Dexterity, 12, 1)]
    public void GetModifierOf_WhenCalled_ReturnsMatchingModifier(AbilityType abilityType, int abilityScoreValue, int expectedModifier)
    {
        // Arrange
        var abilityScores = new AbilityScores
        {
            Strength = new AbilityScore { Value = abilityScoreValue, Type = AbilityType.Strength },
            Dexterity = new AbilityScore { Value = abilityScoreValue, Type = AbilityType.Dexterity },
            Constitution = new AbilityScore { Value = abilityScoreValue, Type = AbilityType.Constitution },
            Wisdom = new AbilityScore { Value = abilityScoreValue, Type = AbilityType.Wisdom },
            Intelligence = new AbilityScore { Value = abilityScoreValue, Type = AbilityType.Intelligence },
            Charisma = new AbilityScore { Value = abilityScoreValue, Type = AbilityType.Charisma }
        };

        // Act
        var realModifier = abilityScores.GetModifierOf(abilityType);

        // Assert
        Assert.AreEqual(expectedModifier, realModifier);
    }

    [TestMethod]
    public void GetModifierOf_WithTwoMatchingScores_Throws()
    {
        // Arrange
        var abilityScores = new AbilityScores
        {
            Strength = new AbilityScore { Value = 10, Type = AbilityType.Strength },
            Dexterity = new AbilityScore { Value = 10, Type = AbilityType.Strength },
        };

        // Act and Assert
        Assert.ThrowsException<InvalidOperationException>( () => abilityScores.GetModifierOf(AbilityType.Strength));
    }
}