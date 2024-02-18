using FluentValidation;

namespace DnDProbabilityCalculator.Shared.Party;

public class CreatePartyDtoValidator : AbstractValidator<CreatePartyDto>
{
    public CreatePartyDtoValidator()
    {
        RuleFor(party => party.Name).NotEmpty().WithMessage("Your party must have a name");
        RuleFor(party => party.Characters).NotEmpty().WithMessage("Your party must have at least one character");
        RuleForEach(party => party.Characters).SetValidator(new CharacterDtoValidator());
    }
}