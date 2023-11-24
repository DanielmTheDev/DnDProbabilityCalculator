namespace DnDProbabilityCalculator.Core.Adventuring.Attack;

public record Weapon(int NumberOfDice, int DiceSides)
{
    public int Bonus { get; set; }
}
