using DnDProbabilityCalculator.Shared.PartyCreation;
using FluentValidation;

namespace DnDProbabilityCalculator.Shared.Party.Validation;

public class CharacterDtoValidator : AbstractValidator<CreateCharacterDto>
{
    private readonly int[] _allowedDiceSides = [4, 6, 8, 10, 12, 20];
    public CharacterDtoValidator()
    {
        RuleFor(character => character.Name).NotEmpty().WithMessage("Your character must have a name");
        RuleFor(character => character.NumberOfAttacks).GreaterThan(0).WithMessage("The number of attacks must be at least 1");
        RuleFor(character => character.ProficiencyBonus).GreaterThan(1).WithMessage("Proficiency bonus must be at least 2");
        RuleFor(character => character.ArmorClass).GreaterThan(-1).WithMessage("Armor class can't be negative");

        RuleFor(character => character.Strength).InclusiveBetween(0, 30).WithMessage("Strength must be between 0 and 30");
        RuleFor(character => character.Dexterity).InclusiveBetween(0, 30).WithMessage("Dexterity must be between 0 and 30");
        RuleFor(character => character.Constitution).InclusiveBetween(0, 30).WithMessage("Constitution must be between 0 and 30");
        RuleFor(character => character.Intelligence).InclusiveBetween(0, 30).WithMessage("Intelligence must be between 0 and 30");
        RuleFor(character => character.Wisdom).InclusiveBetween(0, 30).WithMessage("Wisdom must be between 0 and 30");
        RuleFor(character => character.Charisma).InclusiveBetween(0, 30).WithMessage("Charisma must be between 0 and 30");

        RuleFor(character => character.NumberOfDamageDice).GreaterThan(0).WithMessage("The number of damage dice must be at least 1");
        RuleFor(character => character.DiceSides).Must(diceSides => _allowedDiceSides.Contains(diceSides)).WithMessage("The damage die number must be one of 4, 6, 8, 10, 12, 20");
    }
}