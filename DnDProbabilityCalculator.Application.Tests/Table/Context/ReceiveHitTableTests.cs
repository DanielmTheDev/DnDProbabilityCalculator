using DnDProbabilityCalculator.Application.Table.Context;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table.Context;

[TestClass]
public class ReceiveHitTableTests
{
    [TestMethod]
    public void FromActor_WhenCalled_ReturnsGetHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = ReceiveHitTable.FromActor(actor, new[] { -1, 0, 1 }, 2);

        // Assert
        new List<string> { "2 ","-1", "0", "1" }.AssertElementsAreContainedIn(tableData.AttackModifiers);
        new List<string> { "1", "38%", "32%", "26%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
        new List<string> { "2", "56%", "64%", "72%" }.AssertElementsAreContainedIn(tableData.Probabilities[1]);
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
                Bonus = 2
            },
            AttackAbility = AbilityScoreType.Charisma
        };
}