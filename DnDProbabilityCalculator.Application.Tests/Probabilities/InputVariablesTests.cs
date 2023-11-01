using DnDProbabilityCalculator.Application.Probabilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Probabilities;

[TestClass]
public class InputVariablesTests
{
    [TestInitialize]
    public void Initialize()
    {
    }

    [TestMethod]
    public void Constructor_WithDifferentNumberOfDcsAndAttackModifiers_Throws()
    {
        // Arrange Act and Assert
        Assert.ThrowsException<ArgumentException>(() => new InputVariables(new[] { 1 }, new[] { 1, 2 }, 3));
    }

    [TestMethod]
    public void WithIncrementedNumberOfAttacks_WhenCalled_IncrementsOnlyNumberOfAttacks()
    {
        // Arrange
        var originalInputVariables = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, 3);

        // Act
        var actual = originalInputVariables.WithIncrementedNumberOfAttacks();

        // Assert
        var expected = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, 4);
        Assert.AreEqual(expected.NumberOfAttacks, actual.NumberOfAttacks);
        CollectionAssert.AreEquivalent(expected.AttackModifiers, actual.AttackModifiers);
        CollectionAssert.AreEquivalent(expected.Dcs, actual.Dcs);
    }

    [TestMethod]
    public void WithDecrementedNumberOfAttacks_WhenCalled_DecrementsOnlyNumberOfAttacks()
    {
        // Arrange
        var originalInputVariables = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, 3);

        // Act
        var actual = originalInputVariables.WithDecrementedNumberOfAttacks();

        // Assert
        var expected = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, 2);
        Assert.AreEqual(expected.NumberOfAttacks, actual.NumberOfAttacks);
        CollectionAssert.AreEquivalent(expected.AttackModifiers, actual.AttackModifiers);
        CollectionAssert.AreEquivalent(expected.Dcs, actual.Dcs);
    }

    [TestMethod]
    public void WithDecrementedDcsAndModifiers_WhenCalled_ShiftsThemToTheLeft()
    {
        // Arrange
        var originalInputVariables = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, 3);

        // Act
        var actual = originalInputVariables.WithDecrementedDcsAndModifiers();

        // Assert
        var expected = new InputVariables(new[] { 0, 1 }, new[] { 0, 1 }, 3);
        Assert.AreEqual(expected.NumberOfAttacks, actual.NumberOfAttacks);
        CollectionAssert.AreEquivalent(expected.AttackModifiers, actual.AttackModifiers);
        CollectionAssert.AreEquivalent(expected.Dcs, actual.Dcs);
    }

    [TestMethod]
    public void WithIncrementedDcsAndModifiers_WhenCalled_ShiftsThemToTheRight()
    {
        // Arrange
        var originalInputVariables = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, 3);

        // Act
        var actual = originalInputVariables.WithIncrementedDcsAndModifiers();

        // Assert
        var expected = new InputVariables(new[] { 2, 3 }, new[] { 2, 3 }, 3);
        Assert.AreEqual(expected.NumberOfAttacks, actual.NumberOfAttacks);
        CollectionAssert.AreEquivalent(expected.AttackModifiers, actual.AttackModifiers);
        CollectionAssert.AreEquivalent(expected.Dcs, actual.Dcs);
    }
}