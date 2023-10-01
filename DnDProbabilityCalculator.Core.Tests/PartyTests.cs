using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Core.Tests;

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
                    "dexterity": 11,
                    "strength": 12,
                    "constitution": 13,
                    "intelligence": 14,
                    "wisdom": 12,
                    "charisma": 14
                  }
                },
                {
                  "name": "Erethil",
                  "attributes": {
                    "dexterity": 1,
                    "strength": 2,
                    "constitution": 3,
                    "intelligence": 4,
                    "wisdom": 5,
                    "charisma": 6
                  }
                }
              ]
            }
           """;
}