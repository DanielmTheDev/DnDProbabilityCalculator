namespace DnDProbabilityCalculator.Core.Tests;

[TestClass]
public class PartyTests
{
    [TestMethod]
    public void FromJsonString_WithValidString_SerializesIntoParty()
    {
        // Act
        var characters = Party.FromJsonString(GetValidJsonString());

        // Assert
        Assert.AreEqual(2, characters.Count());
    }

    private string GetValidJsonString()
        => """
           [
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
           """;
}