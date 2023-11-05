using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table;

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
        new List<string> { "0", "0%", "1%", "2%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
        new List<string> { "1", "10%", "18%", "26%" }.AssertElementsAreContainedIn(tableData.Probabilities[1]);
        new List<string> { "2", "90%", "81%", "72%" }.AssertElementsAreContainedIn(tableData.Probabilities[2]);
    }

    private static Actor GetValidActor()
        => Actor.New()
            .WithName("Durak")
            .WithStrength(10)
            .WithDexterity(12, true)
            .WithConstitution(14)
            .WithWisdom(15)
            .WithIntelligence(16)
            .WithCharisma(8)
            .WithProficiency(9)
            .WithArmorClass(5)
            .WithNumberOfAttacks(2)
            .WithAttackAbility(AbilityScoreType.Charisma)
            .Build();
}