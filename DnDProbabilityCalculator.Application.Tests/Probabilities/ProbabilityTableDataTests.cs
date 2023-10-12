using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DnDProbabilityCalculator.Application.Tests.Probabilities;

[TestClass]
public class ProbabilityTableDataTests
{
    [TestInitialize]
    public void Initialize()
    {

    }

    [TestMethod]
    public void FromActor_WhenCalled_ReturnsProbabilityTableData()
    {
        // Arrange
        var actor = GetValidActor();

        // Act
        var tableData = ProbabilityTableData.FromActor(actor, new[]{10, 12, 14});

        // Assert
        Assert.AreEqual("Durak", tableData.Header);
        CollectionAssert.AreEquivalent(new List<string> { "DC","10", "12", "14" }, tableData.DcRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Str", "0.55", "0.45", "0.35" }, tableData.StrengthRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Dex", "[green]1.05[/]", "[green]0.95[/]", "0.85" }, tableData.DexterityRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Con", "0.65", "0.55", "0.45" }, tableData.ConstitutionRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Wis", "0.65", "0.55", "0.45" }, tableData.WisdomRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Int", "0.7", "0.6", "0.5" }, tableData.IntelligenceRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Cha", "0.5", "0.4", "[red]0.3[/]" }, tableData.CharismaRow.ToList());
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
            .Build();
}