namespace DnDProbabilityCalculator.Core.Adventuring.Attack;

public record AttackProbabilities
{
    public required int ArmorClass { get; set; }
    public required int AttackModifier { get; set; }
    public required List<AttackProbability> Probabilities { get; set; }

    public static AttackProbabilities Create(int attackModifier, int totalNumberOfAttacks, int armorClass)
    {
        var singleHitProbability = (21 - (armorClass - attackModifier)) / 20.0;
        var probabilities = Enumerable.Range(0, totalNumberOfAttacks + 1)
            .Select(currentNumberOfAttacks => CreateAttackProbability(totalNumberOfAttacks, currentNumberOfAttacks, singleHitProbability)).ToList();
        return new()
        {
            ArmorClass = armorClass,
            AttackModifier = attackModifier,
            Probabilities = probabilities
        };
    }

    private static AttackProbability CreateAttackProbability(int totalNumberOfAttacks, int currentNumberOfAttacks, double singleHitProbability)
        => new(currentNumberOfAttacks, CalculateMultipleAttackProbability(totalNumberOfAttacks, currentNumberOfAttacks, singleHitProbability));

    private static double CalculateMultipleAttackProbability(int numberOfAttacks, int numberAttacks, double singleHitProbability)
        => Math.Round(BinomialCoefficient(numberOfAttacks, numberAttacks) * Math.Pow(singleHitProbability, numberAttacks) * Math.Pow(1 - singleHitProbability, numberOfAttacks - numberAttacks), 2);

    private static double Factorial(int n)
        => Enumerable.Range(1, n).Aggregate(1.0, (acc, value) => acc * value);

    private static double BinomialCoefficient(int n, int k)
        => Factorial(n) / (Factorial(k) * Factorial(n - k));
}