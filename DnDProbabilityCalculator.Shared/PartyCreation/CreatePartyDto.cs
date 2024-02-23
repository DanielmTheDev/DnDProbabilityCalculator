using DnDProbabilityCalculator.Core.Adventuring;

namespace DnDProbabilityCalculator.Shared.PartyCreation;

public class CreatePartyDto
{
    public string? Name { get; set; }

    public List<CreateCharacterDto> Characters { get; set; } = [];

    public Party ToParty(string userId)
        => new()
        {
            Id = Guid.NewGuid().ToString(),
            Name = Name!,
            UserId = userId,
            Characters = Characters.Select(character => new Actor
            {
                Id = Guid.NewGuid().ToString(),
                Name = character.Name!,
                ProficiencyBonus = character.ProficiencyBonus,
                ArmorClass = character.ArmorClass,
                NumberOfAttacks = character.NumberOfAttacks,
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