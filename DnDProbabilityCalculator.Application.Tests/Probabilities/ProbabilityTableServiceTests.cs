using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DnDProbabilityCalculator.Application.Tests.Probabilities;

[TestClass]
public class ProbabilityTableServiceTests
{
    private ProbabilityTableService _probabilityTableService = null!;
    private Mock<IPartyRepository> _repositoryMock = null!;

    [TestInitialize]
    public void Initialize()
    {
        _repositoryMock = new();
        _probabilityTableService = new(_repositoryMock.Object);
    }

    [TestMethod]
    public void Get_WithValidDCs_ReturnsMatchingProbabilityTables()
    {
        // Arrange
        _repositoryMock.Setup(repository => repository.Get()).Returns(GetValidParty());

        // Act
        var tables = _probabilityTableService.Get(10, 12, 14);

        // Assert
        Assert.AreEqual("Durak", tables.First().Header);
        CollectionAssert.AreEquivalent(new List<string> { "DC","10", "12", "14" }, tables.First().DcRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Str", "0.55", "0.45", "0.35" }, tables.First().StrengthRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Dex", "[green]1.05[/]", "[green]0.95[/]", "0.85" }, tables.First().DexterityRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Con", "0.65", "0.55", "0.45" }, tables.First().ConstitutionRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Wis", "0.65", "0.55", "0.45" }, tables.First().WisdomRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Int", "0.7", "0.6", "0.5" }, tables.First().IntelligenceRow.ToList());
        CollectionAssert.AreEquivalent(new List<string> { "Cha", "0.5", "0.4", "[red]0.3[/]" }, tables.First().CharismaRow.ToList());
    }

    private static Party GetValidParty()
        => new()
        {
            Characters = new List<Actor>
            {
                Actor.New()
                    .WithName("Durak")
                    .WithStrength(10)
                    .WithDexterity(12, true)
                    .WithConstitution(14)
                    .WithWisdom(15)
                    .WithIntelligence(16)
                    .WithCharisma(8)
                    .WithProficiency(9)
                    .Build()
            }
        };
}