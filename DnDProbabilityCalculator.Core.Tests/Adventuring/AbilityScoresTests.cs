using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class AbilityScoresTests
{
    [TestMethod]
    public void IsProficient_WhenCalled_ReturnsProficiency()
    {
        // Arrange
        var abilityScores = new AbilityScores
        {
            Dexterity = new() { Value = 5, IsProficient = true },
            Strength = new() { Value = 5 },
            Constitution = new() { Value = 5, IsProficient = true },
            Intelligence = new() { Value = 5 },
            Wisdom = new() { Value = 5 },
            Charisma = new() { Value = 5 }
        };

        // Act and Assert
        Assert.IsTrue(abilityScores.IsProficientAt(AbilityScoreType.Dexterity));
        Assert.IsTrue(abilityScores.IsProficientAt(AbilityScoreType.Constitution));
        Assert.IsFalse(abilityScores.IsProficientAt(AbilityScoreType.Strength));
        Assert.IsFalse(abilityScores.IsProficientAt(AbilityScoreType.Intelligence));
        Assert.IsFalse(abilityScores.IsProficientAt(AbilityScoreType.Wisdom));
        Assert.IsFalse(abilityScores.IsProficientAt(AbilityScoreType.Charisma));
    }
}