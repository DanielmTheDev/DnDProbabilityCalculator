using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Probabilities;

[TestClass]
public class ProbabilityTableDataTests
{
    [TestMethod]
    public void FromActor_WithDifferentNumberOfElementsInPassedCollections_ThrowsException()
    {
        // Arrange
        var actor = GetValidActor();

        // Act and Assert
        Assert.ThrowsException<ArgumentException>(() => ProbabilityTable.FromActor(actor, new()
        {
            Dcs = new[] { 1 },
            AttackModifiers = new[] { 1, 2 },
            NumberOfAttacks = 1
        }));
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