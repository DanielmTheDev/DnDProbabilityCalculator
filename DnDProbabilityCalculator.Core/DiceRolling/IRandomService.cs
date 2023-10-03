namespace DnDProbabilityCalculator.Core.DiceRolling;

public interface IRandomService
{
    int Next(int minValue, int maxValue);
}