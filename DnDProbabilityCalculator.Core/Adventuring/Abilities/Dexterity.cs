namespace DnDProbabilityCalculator.Core.Adventuring.Abilities;

public record Dexterity : AbilityScore
{
    public override AbilityScoreType Type => AbilityScoreType.Dexterity;
    public static implicit operator Dexterity(int value) => new() { Value = value };
}