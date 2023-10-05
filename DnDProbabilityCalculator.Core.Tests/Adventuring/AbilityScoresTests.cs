using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class AbilityScoresTests
{
    [TestMethod]
    [DataRow(AbilityType.Strength)]
    [DataRow(AbilityType.Dexterity)]
    public void GetModifierOf_WhenCalled_ReturnsMatchingModifier(AbilityType abilityType)
    {
        // Arrange
        var abilityScores = new AbilityScores
        {
            Strength = new AbilityScore { Value = 5, Type = AbilityType.Strength },
            Dexterity = new AbilityScore { Value = 5, Type = AbilityType.Dexterity },
            Constitution = new AbilityScore { Value = 5, Type = AbilityType.Constitution },
            Wisdom = new AbilityScore { Value = 5, Type = AbilityType.Wisdom },
            Intelligence = new AbilityScore { Value = 5, Type = AbilityType.Intelligence },
            Charisma = new AbilityScore { Value = 5, Type = AbilityType.Charisma }
        };

        // Act
        var realAbilityScore = abilityScores.GetScoreOfType(abilityType);

        // Assert
        var expectedAbilityScore = new AbilityScore { Value = 5, Type = abilityType };
        Assert.AreEqual(expectedAbilityScore, realAbilityScore);
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
        Assert.ThrowsException<InvalidOperationException>( () => abilityScores.GetScoreOfType(AbilityType.Strength));
    }
}