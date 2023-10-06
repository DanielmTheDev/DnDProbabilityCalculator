using DnDProbabilityCalculator.Application.Probabilities;
using DnDProbabilityCalculator.Core.Adventuring;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DnDProbabilityCalculator.Application.Tests.Adventuring;

[TestClass]
public class PartyServiceTests
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
    public void Get_WhenCalled_ReturnsPartyFromRepository()
    {
        // Arrange
        _repositoryMock.Setup(repository => repository.Get()).Returns(GetValidParty());

        // Act
        var party = _probabilityTableService.Get();

        // Assert
        Assert.AreEqual(2, party.Characters.Count);
        Assert.AreEqual(GetValidParty().Characters[0].AbilityScores.Strength, party.Characters[0].AbilityScores.Strength);
        Assert.AreEqual(GetValidParty().Characters[0].AbilityScores.Dexterity, party.Characters[0].AbilityScores.Dexterity);
        Assert.AreEqual(GetValidParty().Characters[0].AbilityScores.Constitution, party.Characters[0].AbilityScores.Constitution);
        Assert.AreEqual(GetValidParty().Characters[0].AbilityScores.Intelligence, party.Characters[0].AbilityScores.Intelligence);
        Assert.AreEqual(GetValidParty().Characters[0].AbilityScores.Wisdom, party.Characters[0].AbilityScores.Wisdom);
        Assert.AreEqual(GetValidParty().Characters[0].AbilityScores.Charisma, party.Characters[0].AbilityScores.Charisma);

        Assert.AreEqual(GetValidParty().Characters[1].AbilityScores.Strength, party.Characters[1].AbilityScores.Strength);
        Assert.AreEqual(GetValidParty().Characters[1].AbilityScores.Dexterity, party.Characters[1].AbilityScores.Dexterity);
        Assert.AreEqual(GetValidParty().Characters[1].AbilityScores.Constitution, party.Characters[1].AbilityScores.Constitution);
        Assert.AreEqual(GetValidParty().Characters[1].AbilityScores.Intelligence, party.Characters[1].AbilityScores.Intelligence);
        Assert.AreEqual(GetValidParty().Characters[1].AbilityScores.Wisdom, party.Characters[1].AbilityScores.Wisdom);
        Assert.AreEqual(GetValidParty().Characters[1].AbilityScores.Charisma, party.Characters[1].AbilityScores.Charisma);
    }

    private static Party GetValidParty()
        => new()
        {
            Characters = new List<Actor>
            {
                Actor.New()
                    .WithStrength(1)
                    .WithDexterity(2, true)
                    .WithConstitution(3)
                    .WithWisdom(4)
                    .WithIntelligence(5)
                    .WithCharisma(6)
                    .WithProficiency(3)
                    .Build(),
                Actor.New()
                    .WithStrength(6)
                    .WithDexterity(5)
                    .WithConstitution(4, true)
                    .WithWisdom(3)
                    .WithIntelligence(2, true)
                    .WithCharisma(1)
                    .WithProficiency(2)
                    .Build()
            }
        };
}