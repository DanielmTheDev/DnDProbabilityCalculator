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
        new List<string> { "DC", "10", "12", "14" }.AssertElementsAreContainedIn(tables.First().DcRow);
        new List<string> { "Str", "55%", "45%", "35%" }.AssertElementsAreContainedIn(tables.First().StrengthRow);
        new List<string> { "Dex", "105%", "95%", "85%" }.AssertElementsAreContainedIn(tables.First().DexterityRow);
        new List<string> { "Con", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tables.First().ConstitutionRow);
        new List<string> { "Wis", "65%", "55%", "45%" }.AssertElementsAreContainedIn(tables.First().WisdomRow);
        new List<string> { "Int", "70%", "60%", "50%" }.AssertElementsAreContainedIn(tables.First().IntelligenceRow);
        new List<string> { "Cha", "50%", "40%", "30%" }.AssertElementsAreContainedIn(tables.First().CharismaRow);
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