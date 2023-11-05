using DnDProbabilityCalculator.Application.Table;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Table;

[TestClass]
public class GetHitTableTests
{
    [TestMethod]
    public void FromActor_WhenCalled_ReturnsGetHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = GetHitTable.FromActor(actor, new[] { -1, 0, 1 }, 2);

        // Assert
        new List<string> { "2 ","-1", "0", "1" }.AssertElementsAreContainedIn(tableData.AttackModifiers);
        new List<string> { "0", "6%", "4%", "2%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
        new List<string> { "1", "38%", "32%", "26%" }.AssertElementsAreContainedIn(tableData.Probabilities[1]);
        new List<string> { "2", "56%", "64%", "72%" }.AssertElementsAreContainedIn(tableData.Probabilities[2]);
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
            .WithAttackAbility(AbilityScoreType.Charisma)
            .Build();
}