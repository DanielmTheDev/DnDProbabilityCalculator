using DnDProbabilityCalculator.Application.Table.ReceiveHit;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table.ReceiveHit;

[TestClass]
public class ReceiveHitTableTests
{
    [TestMethod]
    public void FromActor_WhenCalled_ReturnsGetHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = ReceiveHitTable.FromActor(actor, new(new[] { -1, 0, 1 }, new[] { -1, 0, 1 }, new[] { -1, 0, 1 }, 2, AdvantageType.None));

        // Assert
        Assert.AreEqual(2, tableData.TotalNumberOfAttacks);
        CollectionAssert.AreEquivalent(new List<int> { -1, 0, 1 }, tableData.AttackModifiers);
        Assert.AreEqual(1, tableData.Probabilities[0].NumberOfHits);
        CollectionAssert.AreEquivalent(new List<double> { 0.94, 0.96, 0.98 }, tableData.Probabilities[0].Cells.Select(c => Math.Round(c, 2)).ToList());
        Assert.AreEqual(2, tableData.Probabilities[1].NumberOfHits);
        CollectionAssert.AreEquivalent(new List<double> { 0.56, 0.64, 0.72 }, tableData.Probabilities[1].Cells);
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
            ProficiencyBonus = 9,
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