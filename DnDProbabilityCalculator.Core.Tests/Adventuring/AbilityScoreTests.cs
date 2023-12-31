﻿using DnDProbabilityCalculator.Core.Adventuring.Abilities;

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
        var abilityScore = new Dexterity { Value = abilityScoreValue };

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
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Dexterity { Value = abilityScoreValue });
    }
}