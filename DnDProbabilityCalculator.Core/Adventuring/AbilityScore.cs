namespace DnDProbabilityCalculator.Core.Adventuring;

public record AbilityScore
{
    public int Value { get; init; }
    public int Modifier => (int)Math.Floor((Value - 10) / 2.0);

    public static implicit operator int(AbilityScore attribute) => attribute.Value;
}