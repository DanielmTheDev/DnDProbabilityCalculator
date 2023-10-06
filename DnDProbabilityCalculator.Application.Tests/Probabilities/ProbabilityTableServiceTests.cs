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
        Assert.AreEqual("Durak", tables.First().ActorName);

        Assert.AreEqual(10, tables.First().DCs[0].DC);
        Assert.AreEqual(0.55f, tables.First().DCs[0].StrengthProbability);
        Assert.AreEqual(1.05f, tables.First().DCs[0].DexterityProbability);
        Assert.AreEqual(0.65, tables.First().DCs[0].ConstitutionProbability);
        Assert.AreEqual(0.65f, tables.First().DCs[0].WisdomProbability);
        Assert.AreEqual(0.7f, tables.First().DCs[0].IntelligenceProbability);
        Assert.AreEqual(0.5f, tables.First().DCs[0].CharismaProbability);

        Assert.AreEqual(12, tables.First().DCs[0].DC);
        Assert.AreEqual(0.45f, tables.First().DCs[0].StrengthProbability);
        Assert.AreEqual(0.95f, tables.First().DCs[0].DexterityProbability);
        Assert.AreEqual(0.55, tables.First().DCs[0].ConstitutionProbability);
        Assert.AreEqual(0.55f, tables.First().DCs[0].WisdomProbability);
        Assert.AreEqual(0.6f, tables.First().DCs[0].IntelligenceProbability);
        Assert.AreEqual(0.4f, tables.First().DCs[0].CharismaProbability);

        Assert.AreEqual(14, tables.First().DCs[0].DC);
        Assert.AreEqual(0.35f, tables.First().DCs[0].StrengthProbability);
        Assert.AreEqual(0.85f, tables.First().DCs[0].DexterityProbability);
        Assert.AreEqual(0.45, tables.First().DCs[0].ConstitutionProbability);
        Assert.AreEqual(0.45f, tables.First().DCs[0].WisdomProbability);
        Assert.AreEqual(0.5f, tables.First().DCs[0].IntelligenceProbability);
        Assert.AreEqual(0.3f, tables.First().DCs[0].CharismaProbability);
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