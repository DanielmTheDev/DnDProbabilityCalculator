namespace DnDProbabilityCalculator.Core.Adventuring;

public record AbilityScores
{
    public AbilityScore Dexterity { get; set; } = new();
    public AbilityScore Strength { get; set; } = new();
    public AbilityScore Constitution { get; set; } = new();
    public AbilityScore Intelligence { get; set; } = new();
    public AbilityScore Wisdom { get; set; } = new();
    public AbilityScore Charisma { get; set; } = new();

    public int GetModifierOf(AbilityType abilityType)
        => AsCollection().Single(abilityScore => abilityScore.Type == abilityType).Modifier;

    private IEnumerable<AbilityScore> AsCollection()
        => new[] { Dexterity, Strength, Constitution, Intelligence, Wisdom, Charisma };
}