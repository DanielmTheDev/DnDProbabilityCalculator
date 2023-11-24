using DnDProbabilityCalculator.Application.Table.Context;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table.Context;

[TestClass]
public class DeliverHitTableTests
{
    [TestMethod]
    public void FromActor_WhenCalled_ReturnsDeliverHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = DeliverHitTable.FromActor(actor, new[] { 10, 11, 12 });

        // Assert
        new List<string> { "2 ","10", "11", "12" }.AssertElementsAreContainedIn(tableData.ArmorClasses);
        new List<string> { "1", "10%", "18%", "26%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
        new List<string> { "2", "90%", "81%", "72%" }.AssertElementsAreContainedIn(tableData.Probabilities[1]);
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
            WeaponDamage = new(10, 6),
            AttackAbility = AbilityScoreType.Charisma
        };
}