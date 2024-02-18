namespace DnDProbabilityCalculator.Shared.Party;

public class CreateCharacterDto
{
    public string? Name { get; set; }
    public int NumberOfAttacks { get; set; } = 1;
    public int ProficiencyBonus { get; set; } = 2;
    public int ArmorClass { get; set; } = 10;
}