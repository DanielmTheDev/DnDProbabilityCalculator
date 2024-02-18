using System.ComponentModel.DataAnnotations;

namespace DnDProbabilityCalculator.Shared.Party;

public class CreatePartyDto
{
    [Required]
    public string? Name { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "Your party must have at least one character")]
    public IList<CreateCharacterDto> Characters { get; set; } = [];
}