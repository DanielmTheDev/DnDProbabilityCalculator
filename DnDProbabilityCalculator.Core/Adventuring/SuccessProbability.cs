namespace DnDProbabilityCalculator.Core.Adventuring;

public class Probability
{
    private Probability()
    {
    }

    public static double Calculate(int positiveModifier, int negativeModifier, AdvantageType advantageType)
    {
        var probability = (21 - negativeModifier + positiveModifier) / 20.0;
        var boundedProbability = Math.Min(1.0, Math.Max(0, probability));
        return AdjustForAdvantage(boundedProbability, advantageType);
    }

    private static double AdjustForAdvantage(double probability, AdvantageType advantageType)
        => advantageType switch
        {
            AdvantageType.None => probability,
            AdvantageType.Advantage => 1 - Math.Pow(1 - probability, 2),
            AdvantageType.Disadvantage => Math.Pow(probability, 2),
            _ => throw new ArgumentOutOfRangeException(nameof(advantageType), advantageType, null)
        };
}

public enum AdvantageType
{
    None,
    Advantage,
    Disadvantage
}