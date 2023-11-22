namespace DnDProbabilityCalculator.Core.Adventuring.Abilities;

public record Constitution : AbilityScore
{
    public override AbilityScoreType Type => AbilityScoreType.Constitution;
    public static implicit operator Constitution(int value) => new() { Value = value };
}