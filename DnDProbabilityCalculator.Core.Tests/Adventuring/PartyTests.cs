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
        Assert.AreEqual(11, party.Characters[0].Attributes.Dexterity);
        Assert.AreEqual(12, party.Characters[0].Attributes.Strength);
        Assert.AreEqual(13, party.Characters[0].Attributes.Constitution);
        Assert.AreEqual(14, party.Characters[0].Attributes.Intelligence);
        Assert.AreEqual(12, party.Characters[0].Attributes.Wisdom);
        Assert.AreEqual(14, party.Characters[0].Attributes.Charisma);

        Assert.AreEqual("Erethil", party.Characters[1].Name);
        Assert.AreEqual(1, party.Characters[1].Attributes.Dexterity);
        Assert.AreEqual(2, party.Characters[1].Attributes.Strength);
        Assert.AreEqual(3, party.Characters[1].Attributes.Constitution);
        Assert.AreEqual(4, party.Characters[1].Attributes.Intelligence);
        Assert.AreEqual(5, party.Characters[1].Attributes.Wisdom);
        Assert.AreEqual(6, party.Characters[1].Attributes.Charisma);
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
                  "attributes": {
                    "dexterity": {
                      "value": 11
                    },
                    "strength": {
                      "value": 12
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
                  "attributes": {
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
                      "value": 5
                    },
                    "charisma": {
                      "value": 6
                    }
                  }
                }
              ]
            }
           """;
}