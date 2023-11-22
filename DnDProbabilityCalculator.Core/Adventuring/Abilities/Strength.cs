namespace DnDProbabilityCalculator.Core.Adventuring.Abilities;

public record Strength : AbilityScore
{
    public override AbilityScoreType Type => AbilityScoreType.Strength;
    public static implicit operator Strength(int value) => new() { Value = value };
}