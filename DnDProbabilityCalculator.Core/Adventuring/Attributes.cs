namespace DnDProbabilityCalculator.Core.Adventuring;

public record Attributes
{
    public CharacterAttribute Dexterity { get; set; }
    public CharacterAttribute Strength { get; set; }
    public CharacterAttribute Constitution { get; set; }
    public CharacterAttribute Intelligence { get; set; }
    public CharacterAttribute Wisdom { get; set; }
    public CharacterAttribute Charisma { get; set; }
}

public record CharacterAttribute
{
    public int Value { get; set; }

    public static implicit operator int(CharacterAttribute attribute) => attribute.Value;
}