using FluentValidation;

namespace DnDProbabilityCalculator.Shared.Party;

public class CharacterDtoValidator : AbstractValidator<CreateCharacterDto>
{
    public CharacterDtoValidator()
    {
        RuleFor(character => character.Name).NotEmpty().WithMessage("Your character must have a name");
        RuleFor(character => character.NumberOfAttacks).GreaterThan(0).WithMessage("The number of attacks must be at least 1");
        RuleFor(character => character.ProficiencyBonus).GreaterThan(1).WithMessage("Proficiency bonus must be at least 2");
        RuleFor(character => character.ArmorClass).GreaterThan(-1).WithMessage("Armor class can't be negative");
    }
}