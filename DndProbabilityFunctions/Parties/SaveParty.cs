﻿using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Shared.Party;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace DndProbabilityFunctions.Parties;

public class SaveParty(ILoggerFactory loggerFactory)
{
    private readonly ILogger _logger = loggerFactory.CreateLogger<SaveParty>();

    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    [Function("SaveParty")]
    public async Task<PartyMultiResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
    {
        try
        {
            var partyDto = await JsonSerializer.DeserializeAsync<CreatePartyDto>(req.Body, _jsonSerializerOptions);
            var userId = req.Headers.SingleOrDefault(header => header.Key == "x-ms-client-principal-id").Value?.First();
            if (partyDto is null || userId is null)
            {
                return new()
                {
                    Party = null,
                    HttpResponse = req.CreateResponse(HttpStatusCode.BadRequest)
                };
            }
            var partyEntity = CreateParty(partyDto, userId);

            var dto = new SavePartyResponse(partyEntity.Id);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(dto);

            return new()
            {
                Party = partyEntity,
                HttpResponse = response
            };
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occured in function");
            throw;
        }
    }

    private Party CreateParty(CreatePartyDto partyDto, string userId)
        => new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = partyDto.Name!,
            UserId = userId,
            Characters = partyDto.Characters.Select(character => new Actor
            {
                Id = Guid.NewGuid().ToString(),
                Name = character.Name!,
                ProficiencyBonus = character.ProficiencyBonus,
                ArmorClass = character.ArmorClass,
                NumberOfAttacks = 0,
                AbilityScores = new()
                {
                    Dexterity = character.Dexterity,
                    Strength = character.Strength,
                    Constitution = character.Constitution,
                    Intelligence = character.Intelligence,
                    Wisdom = character.Wisdom,
                    Charisma = character.Charisma
                },
                AttackAbility = character.AttackAbility,
                Weapon = new()
                {
                    NumberOfDice = character.NumberOfDamageDice,
                    DiceSides = character.DiceSides,
                    Bonus = character.Bonus,
                    MiscDamageBonus = character.MiscDamageBonus
                }
            }).ToList()
        };
}