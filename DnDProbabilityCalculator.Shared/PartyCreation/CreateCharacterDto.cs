using DnDProbabilityCalculator.Core.Adventuring;
using DnDProbabilityCalculator.Core.Adventuring.Abilities;

namespace DnDProbabilityCalculator.Shared.PartyCreation;

public class CreateCharacterDto
{
    public string? Name { get; set; }
    public int NumberOfAttacks { get; set; } = 1;
    public int ProficiencyBonus { get; set; } = 2;
    public int ArmorClass { get; set; } = 10;
    public AbilityScoreType AttackAbility { get; set; }

    public int NumberOfDamageDice { get; set; } = 2;
    public int DiceSides { get; set; } = 6;
    public int Bonus { get; set; } = 1;
    public int MiscDamageBonus { get; set; }
    public int Strength { get; set; } = 10;
    public int Dexterity { get; set; } = 10;
    public int Constitution { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public int Wisdom { get; set; } = 10;
    public int Charisma { get; set; } = 10;

    public CreateCharacterDto(Actor character)
    {
        Name = character.Name;
        NumberOfAttacks = character.NumberOfAttacks;
        ProficiencyBonus = character.ProficiencyBonus;
        ArmorClass = character.ArmorClass;
        AttackAbility = character.AttackAbility;
        NumberOfDamageDice = character.Weapon.NumberOfDice;
        DiceSides = character.Weapon.DiceSides;
        Bonus = character.Weapon.Bonus;
        MiscDamageBonus = character.Weapon.MiscDamageBonus;
        Strength = character.AbilityScores.Strength;
        Dexterity = character.AbilityScores.Dexterity;
        Constitution = character.AbilityScores.Constitution;
        Intelligence = character.AbilityScores.Intelligence;
        Wisdom = character.AbilityScores.Wisdom;
        Charisma = character.AbilityScores.Wisdom;
    }

    public CreateCharacterDto()
    {
    }
}