using System.Net.Http.Json;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Shared.PartyCreation;

namespace DnDProbabilityCalculator.Blazor.Communication;

public class PartySaver(IHttpClientFactory clientFactory) : IPartySaver
{
    public async Task<string> Save()
    {
        var client = clientFactory.CreateClient("B2CSandbox.ServerAPI");
        var result = await client.PostAsJsonAsync("api/SaveParty", CreateStaticParty());
        var parsedResult = await result.Content.ReadFromJsonAsync<SavePartyResponse>();
        return parsedResult!.PartyId;
    }

    private static Party CreateStaticParty()
        => new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = $"Party {new Random().Next()}",
            UserId = Guid.NewGuid().ToString(),
            Characters = new List<Actor>
            {
                new()
                {
                    Id = "char1",
                    Name = "Char 1",
                    ProficiencyBonus = 2,
                    ArmorClass = 12,
                    NumberOfAttacks = 1,
                    AbilityScores = new()
                    {
                        Dexterity = new() { Value = 5 },
                        Strength = new() { Value = 5 },
                        Constitution = new() { Value = 7 },
                        Intelligence = new() { Value = 9 },
                        Wisdom = new() { Value = 11 },
                        Charisma = new() { Value = 18 }
                    },
                    AttackAbility = AbilityScoreType.Strength,
                    Weapon = new()
                    {
                        NumberOfDice = 1,
                        DiceSides = 6,
                        Bonus = 0,
                        MiscDamageBonus = 0
                    }
                }
            }
        };

}