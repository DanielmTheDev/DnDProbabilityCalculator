using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Probabilities;

[TestClass]
public class GetHitTableTests
{
    [TestMethod]
    public void FromActor_WhenCalled_ReturnsGetHitChanceTable()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = GetHitTable.FromActor(actor, new[] { -1, 0, 1 }, 1);

        // Assert
        new List<string> { "#Attacks/Modifier", "-1", "0", "1" }.AssertElementsAreContainedIn(tableData.AttackModifiers);
        new List<string> { "1", "75%", "80%", "85%" }.AssertElementsAreContainedIn(tableData.Probabilities[0]);
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
            .Build();
}