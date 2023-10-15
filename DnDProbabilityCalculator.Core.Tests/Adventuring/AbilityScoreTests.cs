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

    [TestMethod]
    [DataRow(-1)]
    [DataRow(0)]
    [DataRow(31)]
    [DataRow(int.MaxValue)]
    public void SetValue_WithInvalidAbilityScore_ThrowsOutOfRangeException(int abilityScoreValue)
    {
        // Arrange, Act and Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new AbilityScore { Value = abilityScoreValue });
    }

    [TestMethod]
    [DataRow("Dex", AbilityScoreType.Dexterity)]
    [DataRow("Str", AbilityScoreType.Strength)]
    [DataRow("Con", AbilityScoreType.Constitution)]
    [DataRow("Int", AbilityScoreType.Intelligence)]
    [DataRow("Cha", AbilityScoreType.Charisma)]
    [DataRow("Wis", AbilityScoreType.Wisdom)]
    public void GetAbbreviation_WhenCalled_ReturnsAbbreviationOfType(string expectedAbbreviation, AbilityScoreType abilityScoreType)
    {
        // Arrange
        var abilityScore = new AbilityScore { Value = 5, Type = abilityScoreType };

        // Act
        var abbreviation = abilityScore.Abbreviation;

        // Assert
        Assert.AreEqual(expectedAbbreviation, abbreviation);
    }
}