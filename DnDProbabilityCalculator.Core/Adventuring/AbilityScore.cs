namespace DnDProbabilityCalculator.Core.Adventuring;

public record AbilityScore
{
    public AbilityType Type { get; set; }
    public int Value { get; init; }
    public bool IsProficient { get; init; }
    public int Modifier => (int)Math.Floor((Value - 10) / 2.0);

    public static implicit operator int(AbilityScore attribute) => attribute.Value;
}