namespace DnDProbabilityCalculator.Core.Adventuring;

public class Character
{
    public string Name { get; set; } = string.Empty;
    public Attributes Attributes { get; set; } = new();
}