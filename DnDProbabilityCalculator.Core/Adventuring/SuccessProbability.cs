namespace DnDProbabilityCalculator.Core.Adventuring;

public class SuccessProbability
{
    private SuccessProbability()
    {
    }

    public static double Calculate(int positiveModifier, int negativeModifier, AdvantageType advantage)
    {
        var probability = (21 - negativeModifier + positiveModifier) / 20.0;
        var boundedProbability = Math.Min(1.0, Math.Max(0, probability));
        return Math.Round(AdjustForAdvantage(boundedProbability, advantage), 2);
    }

    private static double AdjustForAdvantage(double probability, AdvantageType advantage)
        => advantage switch
        {
            AdvantageType.None => probability,
            AdvantageType.Advantage => 1 - Math.Pow(1 - probability, 2),
            AdvantageType.Disadvantage => Math.Pow(probability, 2),
            _ => throw new ArgumentOutOfRangeException(nameof(advantage), advantage, null)
        };
}