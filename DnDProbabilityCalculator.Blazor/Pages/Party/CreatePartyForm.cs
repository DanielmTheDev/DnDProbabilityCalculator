using System.ComponentModel.DataAnnotations;

namespace DnDProbabilityCalculator.Blazor.Pages.Party;

public class CreatePartyForm
{
    [Required]
    public string? Name { get; set; }

    [Required]
    [MinLength(1, ErrorMessage = "Your party must have at least one character")]
    public IList<FormCharacter> Characters { get; set; } = [];
}