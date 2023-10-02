using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class AbilityScoreTests
{
    [TestMethod]
    [DataRow(-9, -1)]
    [DataRow(-8, -2)]
    [DataRow(10, 0)]
    [DataRow(11, 0)]
    [DataRow(14, 2)]
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