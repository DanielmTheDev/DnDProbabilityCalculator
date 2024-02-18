using System.ComponentModel.DataAnnotations;

namespace DnDProbabilityCalculator.Shared.Party;

public class CreateCharacterDto
{
    [Required]
    public string? Name { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "The number of attacks must be at least 1")]
    public int NumberOfAttacks { get; set; } = 1;

    public int ProficiencyBonus { get; set; } = 2;
    public int ArmorClass { get; set; } = 10;
}