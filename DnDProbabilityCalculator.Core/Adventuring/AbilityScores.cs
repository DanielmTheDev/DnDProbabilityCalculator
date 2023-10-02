namespace DnDProbabilityCalculator.Core.Adventuring;

public record AbilityScores
{
    public AbilityScore Dexterity { get; init; } = new();
    public AbilityScore Strength { get; init; } = new();
    public AbilityScore Constitution { get; init; } = new();
    public AbilityScore Intelligence { get; init; } = new();
    public AbilityScore Wisdom { get; init; } = new();
    public AbilityScore Charisma { get; init; } = new();
}