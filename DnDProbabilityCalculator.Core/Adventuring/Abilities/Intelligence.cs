namespace DnDProbabilityCalculator.Core.Adventuring.Abilities;

public record Intelligence : AbilityScore
{
    public override AbilityScoreType Type => AbilityScoreType.Intelligence;
    public static implicit operator Intelligence(int value) => new() { Value = value };
}