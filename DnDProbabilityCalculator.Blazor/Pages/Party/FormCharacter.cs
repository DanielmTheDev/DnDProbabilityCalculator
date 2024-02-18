using System.ComponentModel.DataAnnotations;

namespace DnDProbabilityCalculator.Blazor.Pages.Party;

public class FormCharacter
{
    [Required]
    public string? Name { get; set; }
}