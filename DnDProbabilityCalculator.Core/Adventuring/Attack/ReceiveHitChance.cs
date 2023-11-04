namespace DnDProbabilityCalculator.Core.Adventuring.Attack;

public record ReceiveHitChance
{
    public required int NumberOfHits { get; init; }
    public required int ArmorClass { get; set; }
    public required int AttackModifier { get; set; }
    public required double Probability { get; init; }

    public static ReceiveHitChance Create(int attackModifier, int armorClass, int totalNumberOfAttacks, int numberOfHits)
    {
        var singleHitProbability = (21 - (armorClass - attackModifier)) / 20.0;
        return new()
        {
            AttackModifier = attackModifier,
            ArmorClass = armorClass,
            NumberOfHits = numberOfHits,
            Probability = CalculateMultipleAttackProbability(totalNumberOfAttacks, numberOfHits, singleHitProbability)
        };
    }

    private static double CalculateMultipleAttackProbability(int numberOfAttacks, int numberAttacks, double singleHitProbability)
        => Math.Round(BinomialCoefficient(numberOfAttacks, numberAttacks) * Math.Pow(singleHitProbability, numberAttacks) * Math.Pow(1 - singleHitProbability, numberOfAttacks - numberAttacks), 2);

    private static double Factorial(int n)
        => Enumerable.Range(1, n).Aggregate(1.0, (acc, value) => acc * value);

    private static double BinomialCoefficient(int n, int k)
        => Factorial(n) / (Factorial(k) * Factorial(n - k));
}