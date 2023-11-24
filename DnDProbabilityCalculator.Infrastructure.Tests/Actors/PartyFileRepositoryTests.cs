using System.Text.Json;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Infrastructure.Actors;
using DnDProbabilityCalculator.Infrastructure.FileSystem;
using DnDProbabilityCalculator.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DnDProbabilityCalculator.Infrastructure.Tests.Actors;

[TestClass]
public class PartyFileRepositoryTests
{
    private const string FILE_PATH = "file/path";
    private PartyFileRepository _repository = null!;
    private Mock<IFileAccessor> _fileAccessorMock = null!;
    private Mock<IOptions<FileRepositoryOptions>> _optionsMock = null!;

    [TestInitialize]
    public void Initialize()
    {
        _fileAccessorMock = new();
        _optionsMock = new();
        _optionsMock.Setup(option => option.Value).Returns(new FileRepositoryOptions { FilePath = FILE_PATH });
        _repository = new(_fileAccessorMock.Object, _optionsMock.Object);
    }

    [TestMethod]
    public void Get_WithValidFilePath_SerializesIntoParty()
    {
        // Arrange
        _fileAccessorMock.Setup(accessor => accessor.ReadAllText(FILE_PATH)).Returns(GetValidJsonString());

        // Act
        var party = _repository.Get();

        // Assert
        Assert.AreEqual(2, party.Characters.Count);

        Assert.AreEqual("Durak", party.Characters[0].Name);
        Assert.AreEqual(11, party.Characters[0].AbilityScores.Dexterity.Value);
        Assert.AreEqual(12, party.Characters[0].AbilityScores.Strength.Value);
        Assert.AreEqual(13, party.Characters[0].AbilityScores.Constitution.Value);
        Assert.AreEqual(14, party.Characters[0].AbilityScores.Intelligence.Value);
        Assert.AreEqual(12, party.Characters[0].AbilityScores.Wisdom.Value);
        Assert.AreEqual(14, party.Characters[0].AbilityScores.Charisma.Value);
        Assert.IsTrue(party.Characters[0].AbilityScores.Strength.IsProficient);
        Assert.IsTrue(party.Characters[0].AbilityScores.Dexterity.IsProficient);
        Assert.AreEqual(3, party.Characters[0].ProficiencyBonus);
        Assert.AreEqual(14, party.Characters[0].ArmorClass);
        Assert.AreEqual(2, party.Characters[0].NumberOfAttacks);
        Assert.AreEqual(2, party.Characters[0].Weapon.NumberOfDice);
        Assert.AreEqual(6, party.Characters[0].Weapon.DiceSides);
        Assert.AreEqual(2, party.Characters[0].Weapon.Bonus);
        Assert.AreEqual(AbilityScoreType.Strength, party.Characters[0].AttackAbility);

        Assert.AreEqual("Erethil", party.Characters[1].Name);
        Assert.AreEqual(1, party.Characters[1].AbilityScores.Dexterity.Value);
        Assert.AreEqual(2, party.Characters[1].AbilityScores.Strength.Value);
        Assert.AreEqual(3, party.Characters[1].AbilityScores.Constitution.Value);
        Assert.AreEqual(4, party.Characters[1].AbilityScores.Intelligence.Value);
        Assert.AreEqual(5, party.Characters[1].AbilityScores.Wisdom.Value);
        Assert.AreEqual(6, party.Characters[1].AbilityScores.Charisma.Value);
        Assert.IsTrue(party.Characters[1].AbilityScores.Wisdom.IsProficient);
        Assert.IsTrue(party.Characters[1].AbilityScores.Charisma.IsProficient);
        Assert.AreEqual(2, party.Characters[1].ProficiencyBonus);
        Assert.AreEqual(10, party.Characters[1].ArmorClass);
        Assert.AreEqual(4, party.Characters[1].NumberOfAttacks);
        Assert.AreEqual(1, party.Characters[1].Weapon.NumberOfDice);
        Assert.AreEqual(12, party.Characters[1].Weapon.DiceSides);
        Assert.AreEqual(4, party.Characters[1].Weapon.Bonus);
        Assert.AreEqual(AbilityScoreType.Dexterity, party.Characters[1].AttackAbility);
    }

    [TestMethod]
    public void Get_WithInvalidFile_ThrowsFileNotFoundException()
    {
        // Arrange
        _fileAccessorMock.Setup(accessor => accessor.ReadAllText(It.IsAny<string>())).Throws<FileNotFoundException>();

        // Act and Assert
        Assert.ThrowsException<FileNotFoundException>(() => _repository.Get());
    }

    [TestMethod]
    public void Get_WithInvalidFileFormat_ThrowsJsonException()
    {
        // Arrange
        _fileAccessorMock.Setup(accessor => accessor.ReadAllText(It.IsAny<string>())).Returns("Some invalid json");

        // Act and Assert
        Assert.ThrowsException<JsonException>(() => _repository.Get());
    }

    private static string GetValidJsonString()
        => """
            {
              "characters": [
                {
                  "name": "Durak",
                  "proficiencyBonus": 3,
                  "numberOfAttacks": 2,
                  "armorClass": 14,
                  "attackAbility": "Strength",
                  "weapon": {
                    "numberOfDice": 2,
                    "diceSides": 6,
                    "bonus": 2
                  },
                  "abilityScores": {
                    "dexterity": {
                      "value": 11,
                      "isProficient": true
                    },
                    "strength": {
                      "value": 12,
                      "isProficient": true
                    },
                    "constitution": {
                      "value": 13
                    },
                    "intelligence": {
                      "value": 14
                    },
                    "wisdom": {
                      "value": 12
                    },
                    "charisma": {
                      "value": 14
                    }
                  }
                },
                {
                  "name": "Erethil",
                  "proficiencyBonus": 2,
                  "numberOfAttacks": 4,
                  "armorClass": 10,
                  "attackAbility": "Dexterity",
                  "weapon": {
                    "numberOfDice": 1,
                    "diceSides": 12,
                    "bonus": 4
                  },
                  "abilityScores": {
                    "dexterity": {
                      "value": 1
                    },
                    "strength": {
                      "value": 2
                    },
                    "constitution": {
                      "value": 3
                    },
                    "intelligence": {
                      "value": 4
                    },
                    "wisdom": {
                      "value": 5,
                      "isProficient": true
                    },
                    "charisma": {
                      "value": 6,
                      "isProficient": true
                    }
                  }
                }
              ]
            }
           """;
}