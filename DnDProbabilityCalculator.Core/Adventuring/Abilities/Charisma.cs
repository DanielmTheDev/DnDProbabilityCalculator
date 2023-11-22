namespace DnDProbabilityCalculator.Core.Adventuring.Abilities;

public record Charisma : AbilityScore
{
    public override AbilityScoreType Type => AbilityScoreType.Charisma;
    public static implicit operator Charisma(int value) => new() { Value = value };
}