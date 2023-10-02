namespace DnDProbabilityCalculator.Core.Adventuring;

public record AbilityScore
{
    public int Value { get; set; }
    public int Modifier => (Value - 10) / 2;

    public static implicit operator int(AbilityScore attribute) => attribute.Value;
}