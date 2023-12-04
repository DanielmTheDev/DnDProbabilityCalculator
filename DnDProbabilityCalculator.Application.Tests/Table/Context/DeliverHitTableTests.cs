using DnDProbabilityCalculator.Application.Table.Context;
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
        new List<string> { "2 ","10", "11", "12" }.AssertElementsAreContainedIn(tableData.ArmorClasses);
        new List<string> { "1", "32%", "38%", "42%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
        new List<string> { "2", "64%", "56%", "49%" }.AssertElementsAreContainedIn(tableData.Probabilities[1]);
    }

    [TestMethod]
    public void FromActor_WithAdvantage_ReturnsBoundedDeliverHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = DeliverHitTable.FromActor(actor, new(new[] { 10, 11, 12 }, new[] { 10, 11, 12 }, new[] { 10, 11, 12 }, 2, AdvantageType.Advantage));

        // Assert
        new List<string> { "2 ","10", "11", "12" }.AssertElementsAreContainedIn(tableData.ArmorClasses);
        new List<string> { "1", "8%", "12%", "16%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
        new List<string> { "2", "92%", "88%", "83%" }.AssertElementsAreContainedIn(tableData.Probabilities[1]);
    }

    [TestMethod]
    public void FromActor_WithDisadvantage_ReturnsBoundedDeliverHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = DeliverHitTable.FromActor(actor, new(new[] { 10, 11, 12 }, new[] { 10, 11, 12 }, new[] { 10, 11, 12 }, 2, AdvantageType.Disadvantage));

        // Assert
        new List<string> { "2 ","10", "11", "12" }.AssertElementsAreContainedIn(tableData.ArmorClasses);
        new List<string> { "1", "46%", "49%", "50%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
        new List<string> { "2", "41%", "32%", "24%" }.AssertElementsAreContainedIn(tableData.Probabilities[1]);
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