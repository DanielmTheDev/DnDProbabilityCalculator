namespace DnDProbabilityCalculator.Core.Adventuring.Attack;

public record HitChance
{
    public required int NumberOfHits { get; init; }
    public required int ArmorClass { get; set; }
    public required int AttackModifier { get; set; }
    public required double Probability { get; init; }

    public static HitChance Calculate(int attackModifier, int armorClass, int totalNumberOfAttacks, int numberOfHits, AdvantageType advantage)
    {
        var singleHitProbability = Adventuring.Probability.Calculate(attackModifier, armorClass, advantage);

        var multipleHitsProbability = Enumerable.Range(numberOfHits, totalNumberOfAttacks - numberOfHits + 1)
            .Select(currentNumberOfHits => CalculateBoundedMultipleAttackProbability(totalNumberOfAttacks, currentNumberOfHits, singleHitProbability))
            .Aggregate(0.0, (acc, value) => acc + value);

        return new()
        {
            AttackModifier = attackModifier,
            ArmorClass = armorClass,
            NumberOfHits = numberOfHits,
            Probability = multipleHitsProbability
        };
    }

    private static double CalculateBoundedMultipleAttackProbability(int numberOfAttacks, int numberAttacks, double singleHitProbability)
    {
        var probability = Math.Round(BinomialCoefficient(numberOfAttacks, numberAttacks) * Math.Pow(singleHitProbability, numberAttacks) * Math.Pow(1 - singleHitProbability, numberOfAttacks - numberAttacks), 2);
        return Math.Min(1, Math.Max(0, probability));
    }

    private static double Factorial(int n)
        => Enumerable.Range(1, n).Aggregate(1.0, (acc, value) => acc * value);

    private static double BinomialCoefficient(int n, int k)
        => Factorial(n) / (Factorial(k) * Factorial(n - k));
}