using System.Net;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DndProbabilityFunctions;

public class SaveParty(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<SaveParty>();

    [Function("SaveParty")]
    public PartyMultiResponse Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext executionContext)
    {
        _logger.LogInformation("C# HTTP trigger function processed a request.");

        const string message = "Character Saved";

        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
        response.WriteString(message);

        return new()
        {
            Party = CreateStaticParty(),
            HttpResponse = response
        };
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