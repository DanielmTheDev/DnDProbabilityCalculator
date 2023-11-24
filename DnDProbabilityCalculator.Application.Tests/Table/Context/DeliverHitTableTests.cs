using DnDProbabilityCalculator.Application.Table.Context;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table.Context;

[TestClass]
public class DeliverHitTableTests
{
    [TestMethod]
    public void FromActor_WhenCalled_ReturnsRoundedDeliverHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = DeliverHitTable.FromActor(actor, new[] { 10, 11, 12 });

        // Assert
        new List<string> { "2 ","10", "11", "12" }.AssertElementsAreContainedIn(tableData.ArmorClasses);
        new List<string> { "1", "0%", "0%", "10%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
        new List<string> { "2", "100%", "100%", "90%" }.AssertElementsAreContainedIn(tableData.Probabilities[1]);
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