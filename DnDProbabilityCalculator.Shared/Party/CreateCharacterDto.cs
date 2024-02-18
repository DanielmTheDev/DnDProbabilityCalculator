namespace DnDProbabilityCalculator.Shared.Party;

public class CreateCharacterDto
{
    public string? Name { get; set; }
    public int NumberOfAttacks { get; set; } = 1;
    public int ProficiencyBonus { get; set; } = 2;
    public int ArmorClass { get; set; } = 10;
    public int Strength { get; set; } = 10;
    public int Dexterity { get; set; } = 10;
    public int Constitution { get; set; } = 10;
    public int Intelligence { get; set; } = 10;
    public int Wisdom { get; set; } = 10;
    public int Charisma { get; set; } = 10;
}