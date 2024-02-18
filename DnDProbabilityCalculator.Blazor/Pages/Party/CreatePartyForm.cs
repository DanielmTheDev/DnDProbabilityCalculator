using System.ComponentModel.DataAnnotations;

namespace DnDProbabilityCalculator.Blazor.Pages.Party;

public class CreatePartyForm
{
    [Required]
    public string? Name { get; set; }

    [MinLength(1, ErrorMessage = "Your party must have at least one character")]
    public ICollection<FormCharacter> Characters { get; set; } = [];
}