﻿using DnDProbabilityCalculator.Application.Table.Context;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table.Context;

[TestClass]
public class DeliverHitTableTests
{
    [TestMethod]
    public void FromActor_WithNoAdvantage_ReturnsBoundedDeliverHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = DeliverHitTable.FromActor(actor, new(new[] { 10, 11, 12 }, new[] { 10, 11, 12 }, new[] { 10, 11, 12 }, 2, AdvantageType.None));

        // Assert
        Assert.AreEqual(2, tableData.TotalNumberOfAttacks);
        CollectionAssert.AreEquivalent(new List<int> { 10, 11, 12 }, tableData.ArmorClasses);
        Assert.AreEqual(1, tableData.Probabilities[0].NumberOfHits);
        CollectionAssert.AreEquivalent(new List<double> { 0.96, 0.94, 0.91 }, tableData.Probabilities[0].Cells.Select(c => Math.Round(c, 2)).ToList());
        Assert.AreEqual(2, tableData.Probabilities[1].NumberOfHits);
        CollectionAssert.AreEquivalent(new List<double> { 0.64, 0.56, 0.49 }, tableData.Probabilities[1].Cells);
    }

    [TestMethod]
    public void FromActor_WithAdvantage_ReturnsBoundedDeliverHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = DeliverHitTable.FromActor(actor, new(new[] { 10, 11, 12 }, new[] { 10, 11, 12 }, new[] { 10, 11, 12 }, 2, AdvantageType.Advantage));

        // Assert
        Assert.AreEqual(2, tableData.TotalNumberOfAttacks);
        CollectionAssert.AreEquivalent(new List<int> { 10, 11, 12 }, tableData.ArmorClasses);
        Assert.AreEqual(1, tableData.Probabilities[0].NumberOfHits);
        CollectionAssert.AreEquivalent(new List<double> { 1, 1, 0.99 }, tableData.Probabilities[0].Cells);
        Assert.AreEqual(2, tableData.Probabilities[1].NumberOfHits);
        CollectionAssert.AreEquivalent(new List<double> { 0.92, 0.88, 0.83 }, tableData.Probabilities[1].Cells);
    }

    [TestMethod]
    public void FromActor_WithDisadvantage_ReturnsBoundedDeliverHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = DeliverHitTable.FromActor(actor, new(new[] { 10, 11, 12 }, new[] { 10, 11, 12 }, new[] { 10, 11, 12 }, 2, AdvantageType.Disadvantage));

        // Assert
        Assert.AreEqual(2, tableData.TotalNumberOfAttacks);
        CollectionAssert.AreEquivalent(new List<int> { 10, 11, 12 }, tableData.ArmorClasses);
        Assert.AreEqual(1, tableData.Probabilities[0].NumberOfHits);
        CollectionAssert.AreEquivalent(new List<double> { 0.87, 0.81, 0.74 }, tableData.Probabilities[0].Cells);
        Assert.AreEqual(2, tableData.Probabilities[1].NumberOfHits);
        CollectionAssert.AreEquivalent(new List<double> { 0.41, 0.32, 0.24 }, tableData.Probabilities[1].Cells);
    }

    private static Actor GetValidActor()
        => new()
        {
            Name = "Durak",
            AbilityScores = new()
            {
                Dexterity = 12,
                Strength = 10,
                Constitution = 14,
                Intelligence = 16,
                Wisdom = 15,
                Charisma = 8
            },
            ProficiencyBonus = 4,
            ArmorClass = 5,
            NumberOfAttacks = 2,
            Weapon = new()
            {
                NumberOfDice = 10,
                DiceSides = 6,
                Bonus = 2,
                MiscDamageBonus = 2
            },
            AttackAbility = AbilityScoreType.Charisma
        };
}