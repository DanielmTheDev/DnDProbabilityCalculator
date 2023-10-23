using DnDProbabilityCalculator.Core.Adventuring.Abilities;
using DnDProbabilityCalculator.Core.Adventuring.ActorBuilder;
using DnDProbabilityCalculator.Core.Adventuring.Probabilities;

namespace DnDProbabilityCalculator.Core.Adventuring;

public class Actor
{
    public string Name { get; set; } = string.Empty;
    public AbilityScores AbilityScores { get; init; } = new();
    public int ProficiencyBonus { get; set; }
    public int ArmorClass { get; set; }

    public static INameStage New()
        => new NameStage(new());

    public double SavingThrowSuccessChance(AbilityScoreType abilityScoreType, int dc)
    {
        var abilityScore = AbilityScores.Get(abilityScoreType);
        var proficiencyBonus = abilityScore.IsProficient ? ProficiencyBonus : 0;
        return CalculateSavingThrowSuccessChance(dc, abilityScore, proficiencyBonus);
    }

    public double GetHitChance(int modifier)
        => (21d - (ArmorClass - modifier)) / 20;

    public List<AttackProbability> CalculateHitProbabilities(int attackModifier, int totalNumberOfAttacks)
    {
        var singleHitProbability = (21 - (ArmorClass - attackModifier)) / 20.0;
        return Enumerable.Range(0, totalNumberOfAttacks + 1)
            .Select(currentNumberOfAttacks => CreateAttackProbability(totalNumberOfAttacks, currentNumberOfAttacks, singleHitProbability)).ToList();
    }

    private static AttackProbability CreateAttackProbability(int totalNumberOfAttacks, int currentNumberOfAttacks, double singleHitProbability)
        => new(currentNumberOfAttacks, CalculateMultipleAttackProbability(totalNumberOfAttacks, currentNumberOfAttacks, singleHitProbability));

    private static double CalculateMultipleAttackProbability(int numberOfAttacks, int numberAttacks, double singleHitProbability)
        => Math.Round(BinomialCoefficient(numberOfAttacks, numberAttacks) * Math.Pow(singleHitProbability, numberAttacks) * Math.Pow(1 - singleHitProbability, numberOfAttacks - numberAttacks), 2);

    private static double Factorial(int n)
        => Enumerable.Range(1, n).Aggregate(1.0, (acc, value) => acc * value);

    private static double BinomialCoefficient(int n, int k)
        => Factorial(n) / (Factorial(k) * Factorial(n - k));

    private static double CalculateSavingThrowSuccessChance(int dc, AbilityScore abilityScore, int proficiencyBonus)
        => (21d + abilityScore.Modifier + proficiencyBonus - dc) / 20;
}