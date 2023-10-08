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

        Assert.AreEqual(10, tables.First().DcProbabilities[0].DC);
        Assert.AreEqual(0.55d, tables.First().DcProbabilities[0].StrengthProbability);
        Assert.AreEqual(1.05d, tables.First().DcProbabilities[0].DexterityProbability);
        Assert.AreEqual(0.65, tables.First().DcProbabilities[0].ConstitutionProbability);
        Assert.AreEqual(0.65d, tables.First().DcProbabilities[0].WisdomProbability);
        Assert.AreEqual(0.7d, tables.First().DcProbabilities[0].IntelligenceProbability);
        Assert.AreEqual(0.5d, tables.First().DcProbabilities[0].CharismaProbability);

        Assert.AreEqual(12, tables.First().DcProbabilities[1].DC);
        Assert.AreEqual(0.45d, tables.First().DcProbabilities[1].StrengthProbability);
        Assert.AreEqual(0.95d, tables.First().DcProbabilities[1].DexterityProbability);
        Assert.AreEqual(0.55, tables.First().DcProbabilities[1].ConstitutionProbability);
        Assert.AreEqual(0.55d, tables.First().DcProbabilities[1].WisdomProbability);
        Assert.AreEqual(0.6d, tables.First().DcProbabilities[1].IntelligenceProbability);
        Assert.AreEqual(0.4d, tables.First().DcProbabilities[1].CharismaProbability);

        Assert.AreEqual(14, tables.First().DcProbabilities[2].DC);
        Assert.AreEqual(0.35d, tables.First().DcProbabilities[2].StrengthProbability);
        Assert.AreEqual(0.85d, tables.First().DcProbabilities[2].DexterityProbability);
        Assert.AreEqual(0.45, tables.First().DcProbabilities[2].ConstitutionProbability);
        Assert.AreEqual(0.45d, tables.First().DcProbabilities[2].WisdomProbability);
        Assert.AreEqual(0.5d, tables.First().DcProbabilities[2].IntelligenceProbability);
        Assert.AreEqual(0.3d, tables.First().DcProbabilities[2].CharismaProbability);
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