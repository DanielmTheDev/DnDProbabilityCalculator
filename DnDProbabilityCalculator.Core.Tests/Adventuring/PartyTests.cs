using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Core.Tests.Adventuring;

[TestClass]
public class PartyTests
{
    [TestMethod]
    public void FromJsonString_WithValidString_SerializesIntoParty()
    {
        // Arrange and Act
        var party = Party.FromJsonString(GetValidJsonString());

        // Assert
        Assert.AreEqual(2, party.Characters.Count);

        Assert.AreEqual("Durak", party.Characters[0].Name);
        Assert.AreEqual(11, party.Characters[0].AbilityScores.Dexterity);
        Assert.AreEqual(12, party.Characters[0].AbilityScores.Strength);
        Assert.AreEqual(13, party.Characters[0].AbilityScores.Constitution);
        Assert.AreEqual(14, party.Characters[0].AbilityScores.Intelligence);
        Assert.AreEqual(12, party.Characters[0].AbilityScores.Wisdom);
        Assert.AreEqual(14, party.Characters[0].AbilityScores.Charisma);
        Assert.IsTrue(party.Characters[0].AbilityScores.Strength.IsProficient);
        Assert.IsTrue(party.Characters[0].AbilityScores.Dexterity.IsProficient);
        Assert.AreEqual(3, party.Characters[0].ProficiencyBonus);

        Assert.AreEqual("Erethil", party.Characters[1].Name);
        Assert.AreEqual(1, party.Characters[1].AbilityScores.Dexterity);
        Assert.AreEqual(2, party.Characters[1].AbilityScores.Strength);
        Assert.AreEqual(3, party.Characters[1].AbilityScores.Constitution);
        Assert.AreEqual(4, party.Characters[1].AbilityScores.Intelligence);
        Assert.AreEqual(5, party.Characters[1].AbilityScores.Wisdom);
        Assert.AreEqual(6, party.Characters[1].AbilityScores.Charisma);
        Assert.IsTrue(party.Characters[1].AbilityScores.Wisdom.IsProficient);
        Assert.IsTrue(party.Characters[1].AbilityScores.Charisma.IsProficient);
        Assert.AreEqual(2, party.Characters[1].ProficiencyBonus);
    }

    public static void FromJsonString_Throws_WithInvalidJsonString()
    {
        // Arrange
        const string jsonSting = "Some invalid json";

        // Act and Assert
        var message = Assert.ThrowsException<FormatException>(() => Party.FromJsonString(jsonSting)).Message;
        Assert.IsTrue(message.Contains(ErrorMessages.Wrong_File_Format));
    }

    private static string GetValidJsonString()
        => """
            {
              "characters": [
                {
                  "name": "Durak",
                  "proficiencyBonus": 3,
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