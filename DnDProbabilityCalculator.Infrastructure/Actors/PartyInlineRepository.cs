﻿using System.Text.Json;
using System.Text.Json.Serialization;
using DnDProbabilityCalculator.Core;
using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Infrastructure.Actors;

public class PartyInlineRepository : IPartyRepository
{
  private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public Party Get()
      => JsonSerializer.Deserialize<Party>(GetStaticJsonString(), JsonSerializerOptions)!;

    private static string GetStaticJsonString()
      => """
         {
           "characters": [
             {
               "name": "Durak",
               "proficiencyBonus": 3,
               "armorClass": 20,
               "numberOfAttacks": 2,
               "attackAbility": "Strength",
               "weapon": {
                 "numberOfDice": 2,
                 "diceSides": 6,
                 "bonus": 1,
                 "miscDamageBonus": 2
               },
               "abilityScores": {
                 "dexterity": {
                   "value": 15
                 },
                 "strength": {
                   "value": 18
                 },
                 "constitution": {
                   "value": 14
                 },
                 "intelligence": {
                   "value": 10
                 },
                 "wisdom": {
                   "value": 11,
                   "isProficient": true
                 },
                 "charisma": {
                   "value": 16,
                   "isProficient": true
                 }
               }
             },
             {
               "name": "Erethil",
               "proficiencyBonus": 3,
               "armorClass": 17,
               "numberOfAttacks": 3,
               "weapon": {
                 "numberOfDice": 1,
                 "diceSides": 6,
                 "bonus": 1,
                 "miscDamageBonus": 0
               },
               "attackAbility": "Dexterity",
               "abilityScores": {
                 "dexterity": {
                   "value": 18,
                   "isProficient": true
                 },
                 "strength": {
                   "value": 13,
                   "isProficient": true
                 },
                 "constitution": {
                   "value": 12
                 },
                 "intelligence": {
                   "value": 14
                 },
                 "wisdom": {
                   "value": 17
                 },
                 "charisma": {
                   "value": 14
                 }
               }
             },
             {
               "name": "Gorak'Thor",
               "proficiencyBonus": 3,
               "armorClass": 20,
               "numberOfAttacks": 1,
               "attackAbility": "Strength",
               "weapon": {
                 "numberOfDice": 2,
                 "diceSides": 6,
                 "bonus": 0,
                 "miscDamageBonus": 0
               },
               "abilityScores": {
                 "dexterity": {
                   "value": 14
                 },
                 "strength": {
                   "value": 13
                 },
                 "constitution": {
                   "value": 18
                 },
                 "intelligence": {
                   "value": 15
                 },
                 "wisdom": {
                   "value": 16,
                   "isProficient": true
                 },
                 "charisma": {
                   "value": 12,
                   "isProficient": true
                 }
               }
             }
           ]
         }
         """;
}