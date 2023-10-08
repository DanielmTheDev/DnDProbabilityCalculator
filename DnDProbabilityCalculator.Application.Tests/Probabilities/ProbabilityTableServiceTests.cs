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

        CollectionAssert.AreEquivalent(new List<int> { 10, 12, 14 }, tables.First().DCs.ToList());
        CollectionAssert.AreEquivalent(new List<double> { 0.55d, 0.45d, 0.35d }, tables.First().StrengthProbabilities.ToList());
        CollectionAssert.AreEquivalent(new List<double> { 1.05d, 0.95d, 0.85d }, tables.First().DexterityProbabilities.ToList());
        CollectionAssert.AreEquivalent(new List<double> { 0.65d, 0.55d, 0.45d }, tables.First().ConstitutionProbabilities.ToList());
        CollectionAssert.AreEquivalent(new List<double> { 0.65d, 0.55d, 0.45d }, tables.First().WisdomProbabilities.ToList());
        CollectionAssert.AreEquivalent(new List<double> { 0.7d, 0.6d, 0.5d }, tables.First().IntelligenceProbabilities.ToList());
        CollectionAssert.AreEquivalent(new List<double> { 0.5d, 0.4d, 0.3d }, tables.First().CharismaProbabilities.ToList());
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