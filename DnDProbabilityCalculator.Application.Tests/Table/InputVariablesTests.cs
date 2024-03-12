using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table;

[TestClass]
public class InputVariablesTests
{
    [TestInitialize]
    public void Initialize()
    {
    }

    [TestMethod]
    [DataRow(new[] { 1, 2 }, new[] { 1, 2 }, new[] { 1 })]
    [DataRow(new[] { 1 }, new[] { 1, 2 }, new[] { 1, 2 })]
    [DataRow(new[] { 1, 2 }, new[] { 1 }, new[] { 1, 2 })]
    public void Constructor_WithDifferentNumberOfElements_Throws(int[] dcs, int[] attackModifiers, int[] armorClasses)
    {
        // Arrange Act and Assert
        Assert.ThrowsException<ArgumentException>(() => new InputVariables(dcs, attackModifiers, armorClasses, 1, AdvantageType.None));
    }

    [TestMethod]
    public void WithIncrementedNumberOfAttacks_WhenCalled_IncrementsOnlyNumberOfAttacks()
    {
        // Arrange
        var originalInputVariables = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, new[] { 1, 2 }, 3, AdvantageType.None);

        // Act
        var actual = originalInputVariables.WithIncrementedNumberOfAttacks();

        // Assert
        var expected = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, new[] { 1, 2 }, 4, AdvantageType.None);
        Assert.AreEqual(expected.NumberOfAttacks, actual.NumberOfAttacks);
        CollectionAssert.AreEquivalent(expected.AttackModifiers, actual.AttackModifiers);
        CollectionAssert.AreEquivalent(expected.Dcs, actual.Dcs);
        CollectionAssert.AreEquivalent(expected.ArmorClasses, actual.ArmorClasses);
    }

    [TestMethod]
    public void WithDecrementedNumberOfAttacks_WhenCalled_DecrementsOnlyNumberOfAttacks()
    {
        // Arrange
        var originalInputVariables = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, new[] { 1, 2 }, 3, AdvantageType.None);

        // Act
        var actual = originalInputVariables.WithDecrementedNumberOfAttacks();

        // Assert
        var expected = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, new[] { 1, 2 }, 2, AdvantageType.None);
        Assert.AreEqual(expected.NumberOfAttacks, actual.NumberOfAttacks);
        CollectionAssert.AreEquivalent(expected.AttackModifiers, actual.AttackModifiers);
        CollectionAssert.AreEquivalent(expected.ArmorClasses, actual.ArmorClasses);
        CollectionAssert.AreEquivalent(expected.Dcs, actual.Dcs);
    }

    [TestMethod]
    public void WithDecrementedColumns_WhenCalled_ShiftsThemToTheLeft()
    {
        // Arrange
        var originalInputVariables = new InputVariables(new[] { 1, 2 }, new[] { 1, 2 }, new[] { 1, 2 }, 3, AdvantageType.None);

        // Act
        var actual = originalInputVariables.WithDecrementedColumns();

        // Assert
        var expected = new InputVariables(new[] { 0, 1 }, new[] { 0, 1 }, new[] { 0, 1 }, 3, AdvantageType.None);
        Assert.AreEqual(expected.NumberOfAttacks, actual.NumberOfAttacks);
        CollectionAssert.AreEquivalent(expected.AttackModifiers, actual.AttackModifiers);
        CollectionAssert.AreEquivalent(expected.ArmorClasses, actual.ArmorClasses);
        CollectionAssert.AreEquivalent(expected.Dcs, actual.Dcs);
    }

    [TestMethod]
    public void WithIncrementedDcsAndModifiers_WhenCalled_ShiftsThemToTheRight()
    {
        // Arrange
        var originalInputVariables = new InputVariables([1, 2], [1, 2], [1, 2], 3, AdvantageType.None);

        // Act
        var actual = originalInputVariables.WithIncrementedColumns();

        // Assert
        var expected = new InputVariables([2, 3], [2, 3], [2, 3], 3, AdvantageType.None);
        Assert.AreEqual(expected.NumberOfAttacks, actual.NumberOfAttacks);
        CollectionAssert.AreEquivalent(expected.AttackModifiers, actual.AttackModifiers);
        CollectionAssert.AreEquivalent(expected.ArmorClasses, actual.ArmorClasses);
        CollectionAssert.AreEquivalent(expected.Dcs, actual.Dcs);
    }

    [TestMethod]
    public void WithDecrementedAttack_WhenCalled_DoesNotGoBelow1()
    {
        // Arrange
        var originalInputVariables = new InputVariables([1, 2], [1, 2], [1, 2], 1, AdvantageType.None);

        // Act
        var result = originalInputVariables.WithDecrementedNumberOfAttacks();

        // Assert
        Assert.AreEqual(1, result.NumberOfAttacks);
    }
}