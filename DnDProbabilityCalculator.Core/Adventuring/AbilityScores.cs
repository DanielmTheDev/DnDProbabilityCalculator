namespace DnDProbabilityCalculator.Core.Adventuring;

public record AbilityScores
{
    public AbilityScore Dexterity { get; set; }
    public AbilityScore Strength { get; set; }
    public AbilityScore Constitution { get; set; }
    public AbilityScore Intelligence { get; set; }
    public AbilityScore Wisdom { get; set; }
    public AbilityScore Charisma { get; set; }
}