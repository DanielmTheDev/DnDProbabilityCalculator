namespace DnDProbabilityCalculator.Core.Adventuring.Attack;

public record Weapon
{
    public required int NumberOfDice { get; init; }
    public required int DiceSides { get; init; }
    public required int Bonus { get; init; }
}