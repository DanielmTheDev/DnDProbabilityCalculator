namespace DnDProbabilityCalculator.Core.DiceRolling;

public class RandomService : IRandomService
{
    private readonly Random _random = new();

    public int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);
}