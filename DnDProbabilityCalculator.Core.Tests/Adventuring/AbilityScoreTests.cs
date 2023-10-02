using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class AbilityScoreTests
{
    [TestMethod]
    [DataRow(9, -1)]
    [DataRow(8, -1)]
    [DataRow(7, -2)]
    [DataRow(1, -5)]
    [DataRow(10, 0)]
    [DataRow(11, 0)]
    [DataRow(14, 2)]
    [DataRow(30, 10)]
    public void Modifer_WhenCalled_IsCalculatedFromAbilityScore(int abilityScoreValue, int expectedModifier)
    {
        // Arrange
        var abilityScore = new AbilityScore { Value = abilityScoreValue };

        // Act
        var realModifier = abilityScore.Modifier;

        // Assert
        Assert.AreEqual(expectedModifier, realModifier);
    }
}